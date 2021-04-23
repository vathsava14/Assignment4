﻿// <auto-generated />
using System;
using Assignment4.DataAccess;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Assignment4.Migrations
{
    [DbContext(typeof(Assignment4DbContext))]
    [Migration("20201108220807_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Assignment4.Models.AnnualEnergyConsumption", b =>
                {
                    b.Property<int>("Year")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EnergySourceId")
                        .HasColumnType("int");

                    b.Property<int?>("SectorId")
                        .HasColumnType("int");

                    b.Property<int>("Value")
                        .HasColumnType("int");

                    b.HasKey("Year");

                    b.HasIndex("EnergySourceId");

                    b.HasIndex("SectorId");

                    b.ToTable("AnnualEnergyConsumption");
                });

            modelBuilder.Entity("Assignment4.Models.EnergySource", b =>
                {
                    b.Property<int>("EnergySourceId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SourceName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("EnergySourceId");

                    b.ToTable("EnergySource");
                });

            modelBuilder.Entity("Assignment4.Models.Sector", b =>
                {
                    b.Property<int>("SectorId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("SectorName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SectorId");

                    b.ToTable("Sector");
                });

            modelBuilder.Entity("Assignment4.Models.AnnualEnergyConsumption", b =>
                {
                    b.HasOne("Assignment4.Models.EnergySource", "energysource")
                        .WithMany()
                        .HasForeignKey("EnergySourceId");

                    b.HasOne("Assignment4.Models.Sector", "sector")
                        .WithMany()
                        .HasForeignKey("SectorId");
                });
#pragma warning restore 612, 618
        }
    }
}
