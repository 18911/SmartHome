﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SmartHome.Contexts;

namespace SmartHome.Migrations
{
    [DbContext(typeof(SmartHomeDbContext))]
    partial class SmartHomeDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("SmartHome.Models.Client", b =>
                {
                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Salt")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdClient");

                    b.ToTable("Client");
                });

            modelBuilder.Entity("SmartHome.Models.Device", b =>
                {
                    b.Property<int>("IdDevice")
                        .HasColumnType("int");

                    b.Property<int>("IdRoom")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdDevice");

                    b.ToTable("Device");
                });

            modelBuilder.Entity("SmartHome.Models.Particulates", b =>
                {
                    b.Property<int>("IdParticulates")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDevice")
                        .HasColumnType("int");

                    b.Property<float>("Pm10Value")
                        .HasColumnType("real");

                    b.Property<float>("Pm25Value")
                        .HasColumnType("real");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("IdParticulates");

                    b.ToTable("Particulates");
                });

            modelBuilder.Entity("SmartHome.Models.Room", b =>
                {
                    b.Property<int>("IdRoom")
                        .HasColumnType("int");

                    b.Property<int>("IdClient")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("IdRoom");

                    b.ToTable("Room");
                });

            modelBuilder.Entity("SmartHome.Models.Temperature", b =>
                {
                    b.Property<int>("IdTemperature")
                        .HasColumnType("int");

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime2");

                    b.Property<int>("IdDevice")
                        .HasColumnType("int");

                    b.Property<float>("TempValue")
                        .HasColumnType("real");

                    b.Property<DateTime>("Time")
                        .HasColumnType("datetime2");

                    b.HasKey("IdTemperature");

                    b.ToTable("Temperature");
                });
#pragma warning restore 612, 618
        }
    }
}
