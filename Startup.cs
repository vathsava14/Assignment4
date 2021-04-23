using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using Assignment4.DataAccess;
using Assignment4.Models;
using System.Collections.Specialized;
using System.Collections;
using System.Net.Http;
using System.Text.Json;

namespace Assignment4
{
    public class Startup
    {
        private static OrderedDictionary SECTORS_AND_SERIES_CODES = BuildSectorsAndSeriesCodesDict();

        private static OrderedDictionary ENERGY_SOURCES_AND_SERIES_CODES = BuildEnergySourcesAndSeriesCodesDict();

        private static List<(string, string)> INVALID_SECTOR_AND_ENERGY_SOURCE_COMBINATIONS = new List<(string, string)>
        {
            ("Residential", "Total Petroleum"),
            ("Transportation", "Geothermal Energy"),
            ("Transportation", "Natural Gas"),
            ("Transportation", "Solar Energy"),
            ("Electric Power", "Geothermal Energy"),
            ("Electric Power", "Solar Energy"),
            ("Electric Power", "Total Energy"),
            ("Electric Power", "Total Petroleum")
        };

        private static string BASE_URL = "http://api.eia.gov/series/";

        private static string API_KEY = "1a186bc525e06ab077cd7cde973b5c6a";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddDbContext<Assignment4DbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("Assignment4DbContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Assignment4DbContext>();
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();
                context.Database.SetCommandTimeout(180);

                List<EnergySource> energySources = BuildEnergySources();
                List<Sector> sectors = BuildSectors();
                List<AnnualEnergyConsumption> annualEnergyConsumptionData = GetAnnualEnergyConsumption(energySources, sectors);

                context.EnergySource.AddRange(energySources);
                context.Sector.AddRange(sectors);
                context.AnnualEnergyConsumption.AddRange(annualEnergyConsumptionData);
                context.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static OrderedDictionary BuildSectorsAndSeriesCodesDict()
        {
            OrderedDictionary dict = new OrderedDictionary();
            dict.Add("Residential", "RC");
            dict.Add("Commercial", "CC");
            dict.Add("Industrial", "IC");
            dict.Add("Transportation", "AC");
            dict.Add("Electric Power", "EI");
            return dict;
        }

        private static OrderedDictionary BuildEnergySourcesAndSeriesCodesDict()
        {
            OrderedDictionary dict = new OrderedDictionary();
            dict.Add("Biomass Energy", "BM");
            dict.Add("Coal", "CL");
            dict.Add("Geothermal Energy", "GE");
            dict.Add("Natural Gas", "NN");
            dict.Add("Solar Energy", "SO");
            dict.Add("Total Energy", "TE");
            dict.Add("Total Fossil Fuels", "FF");
            dict.Add("Total Petroleum", "PM");
            dict.Add("Total Primary Energy", "TX");
            dict.Add("Total Renewable Energy", "RE");
            return dict;
        }

        private List<EnergySource> BuildEnergySources()
        {
            List<EnergySource> energySources = new List<EnergySource>();
            foreach (DictionaryEntry entry in ENERGY_SOURCES_AND_SERIES_CODES)
            {
                EnergySource energySource = new EnergySource();
                energySource.SourceName = (string) entry.Key;
                energySources.Add(energySource);
            }

            return energySources;
        }

        private List<Sector> BuildSectors()
        {
            List<Sector> sectors = new List<Sector>();
            foreach (DictionaryEntry entry in SECTORS_AND_SERIES_CODES)
            {
                Sector sector = new Sector();
                sector.SectorName = (string) entry.Key;
                sectors.Add(sector);
            }

            return sectors;
        }

        private string BuildSeriesId(EnergySource energySource, Sector sector)
        {
            return "TOTAL." + ENERGY_SOURCES_AND_SERIES_CODES[energySource.SourceName] + SECTORS_AND_SERIES_CODES[sector.SectorName] + "BUS.A";
        }

        private string BuildUrl(string seriesId)
        {
            return BASE_URL + "?api_key=" + API_KEY + "&series_id=" + seriesId;
        }

        private List<AnnualEnergyConsumption> GetAnnualEnergyConsumption(List<EnergySource> energySources, List<Sector> sectors)
        {
            List<AnnualEnergyConsumption> consumptionList = new List<AnnualEnergyConsumption>();
            for (int i = 0; i < sectors.Count; ++i)
            {
                for (int j = 0; j < energySources.Count; ++j)
                {
                    Sector sector = sectors[i];
                    EnergySource energySource = energySources[j];
                    bool invalidCombination = false;
                    foreach (var pair in INVALID_SECTOR_AND_ENERGY_SOURCE_COMBINATIONS)
                    {
                        if (sector.SectorName.Equals(pair.Item1) && energySource.SourceName.Equals(pair.Item2))
                        {
                            invalidCombination = true;
                            break;
                        }
                    }

                    if (invalidCombination)
                    {
                        continue;
                    }

                    string rawData = GetRawData(BuildUrl(BuildSeriesId(energySource, sector)));
                    JsonDocument document = JsonDocument.Parse(rawData);
                    JsonElement root = document.RootElement;
                    var data = (root.TryGetProperty("series", out var series) ? series[0].GetProperty("data") : root.GetProperty("data")).EnumerateArray();
                    while (data.MoveNext())
                    {
                        var datum = data.Current;
                        AnnualEnergyConsumption consumption = new AnnualEnergyConsumption();
                        consumption.energysource = energySource;
                        consumption.sector = sector;
                        consumption.Year = Convert.ToInt32(datum[0].GetString());
                        if (Convert.ToString(datum[1]) != "NA")
                        {
                            consumption.Value = Convert.ToDecimal(Convert.ToString(datum[1]));
                        }
                        consumptionList.Add(consumption);
                    }
                }
            }

            return consumptionList;
        }

        private string GetRawData(string uri)
        {
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Clear();
            httpClient.DefaultRequestHeaders.Add("X-Api-Key", API_KEY);
            httpClient.DefaultRequestHeaders.Accept.Add(
                new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            httpClient.BaseAddress = new Uri(uri);

            try
            {
                HttpResponseMessage response = httpClient.GetAsync(uri).GetAwaiter().GetResult();
                if (response.IsSuccessStatusCode)
                {
                    return response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                }
                else
                {
                    throw new Exception("Status code is: " + response.StatusCode);
                }
            }
            catch (Exception e)
            {
                // This is a useful place to insert a breakpoint and observe the error message
                Console.WriteLine(e.Message);
                return "";
            }
        }
    }
}
