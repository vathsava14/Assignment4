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
        private static OrderedDictionary County_Codes = CountCountyCodesDict();

        private static OrderedDictionary Population_Codes = CountPopulationDict();

        private static string BASE_URL = "https://opendata.maryland.gov/resource/is7h-kp6x.json?$select=jurisdictions,total_population";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //var connection = @"Server = tcp:disassignment4.database.windows.net,1433; Initial Catalog = Assignment4; Persist Security Info = False; User ID = vathsava; Password = Admin@2020; MultipleActiveResultSets = False; Encrypt = True; TrustServerCertificate = False; Connection Timeout = 30;";
            var connection = "Server=(localdb)\\mssqllocaldb;Database=Assignment4DbContext;Trusted_Connection=True;MultipleActiveResultSets=true";

            services.AddDbContext<Assignment4DbContext>
                (options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<Assignment4DbContext>();
                context.Database.EnsureCreated();

                List<Population> populations = CountPopulations();
                List<County> counties = CountCounties();
                //List<Demographic> demographic = GetDemographic(populations, counties);

                context.Populations.AddRange(populations);
                context.Counties.AddRange(counties);
                //context.Demographics.AddRange(demographic);
                context.SaveChanges();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
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

        private static OrderedDictionary CountCountyCodesDict()
        {
            OrderedDictionary dict = new OrderedDictionary();
            dict.Add("Montgomery County", "MC");
            dict.Add("Prince George's County", "PGC");
            dict.Add("Baltimore County", "BC");
            dict.Add("Anne Arundel County", "AAC");
            dict.Add("Howard County", "HOC");
            dict.Add("Harford County", "HAC");
            dict.Add("Frederick County", "FC");
            dict.Add("Carroll County", "CAC");
            dict.Add("Washington County", "WC");
            dict.Add("Charles County", "CHC");
            return dict;
        }

        private static OrderedDictionary CountPopulationDict()
        {
            OrderedDictionary dict = new OrderedDictionary();
            dict.Add("Total Population", "TP");
            dict.Add("Bachelor's Degree", "BD");
            dict.Add("Graduate or Professional", "GP");
            return dict;
        }

        private List<Population> CountPopulations()
        {
            List<Population> populations = new List<Population>();
            foreach (DictionaryEntry entry in Population_Codes)
            {
                Population population = new Population();
                population.PopTypeName = (string)entry.Key;
                populations.Add(population);
            }

            return populations;
        }

        private List<County> CountCounties()
        {
            List<County> counties = new List<County>();
            foreach (DictionaryEntry entry in County_Codes)
            {
                County county = new County();
                county.CountyName = (string)entry.Key;
                counties.Add(county);
            }

            return counties;
        }

        private string BuildSeriesId(Population population, County county)
        {
            return County_Codes[county.CountyName].ToString();
        }



        /*while (data.MoveNext())
        {
            var datum = data.Current;
            Demographic demographic = new Demographic();
            demographic.county = county;
            demographic.population = population;
            if (Convert.ToString(datum[1]) != "NA")
            {
                demographic.Value = Convert.ToInt32(Convert.ToString(datum[1]));
            }
            demographList.Add(demographic);
        }
    }
}
return demographList;
}
        */
    }
}


/*
 Server=tcp:disassignment4.database.windows.net,1433;Initial Catalog=Assignment4;Persist Security Info=False;User ID=vathsava;Password={your_password};MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;
*/