﻿// <auto-generated />
using System;
using Dashboard.data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace HomePrices.Migrations
{
    [DbContext(typeof(HomePricesContext))]
    [Migration("20231004013930_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "7.0.11");

            modelBuilder.Entity("HomePrices.data.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("PriceDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PriceQuarter")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PriceWeek")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SquareFeet")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("YearBuilt")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Homes");
                });

            modelBuilder.Entity("HomePrices.data.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("State")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("HomeRegion", b =>
                {
                    b.Property<int>("HomeListId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RegionListId")
                        .HasColumnType("INTEGER");

                    b.HasKey("HomeListId", "RegionListId");

                    b.HasIndex("RegionListId");

                    b.ToTable("HomeRegion");
                });

            modelBuilder.Entity("HomeRegion", b =>
                {
                    b.HasOne("HomePrices.data.Home", null)
                        .WithMany()
                        .HasForeignKey("HomeListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("HomePrices.data.Region", null)
                        .WithMany()
                        .HasForeignKey("RegionListId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
