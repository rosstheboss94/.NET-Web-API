﻿// <auto-generated />
using Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Api.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220813172341_UserMigrationAdded")]
    partial class UserMigrationAdded
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.8");

            modelBuilder.Entity("Api.Entities.AppUser", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Api.Entities.Trade", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Entry")
                        .HasColumnType("REAL");

                    b.Property<string>("Notes")
                        .HasColumnType("TEXT");

                    b.Property<string>("Result")
                        .HasColumnType("TEXT");

                    b.Property<string>("RiskReward")
                        .HasColumnType("TEXT");

                    b.Property<double>("StopLoss")
                        .HasColumnType("REAL");

                    b.Property<double>("TakeProfit")
                        .HasColumnType("REAL");

                    b.Property<string>("Ticker")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Trades");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Entry = 1.0259,
                            Notes = "Test Notes",
                            Result = "WIN",
                            RiskReward = "1:3",
                            StopLoss = 1.0145,
                            TakeProfit = 1.05,
                            Ticker = "EUR/USD",
                            Type = 0
                        },
                        new
                        {
                            Id = 2,
                            Entry = 0.71213000000000004,
                            Notes = "Test Notes",
                            Result = "LOSS",
                            RiskReward = "1:5",
                            StopLoss = 0.68000000000000005,
                            TakeProfit = 0.72999999999999998,
                            Ticker = "AUD/USD",
                            Type = 0
                        },
                        new
                        {
                            Id = 3,
                            Entry = 1.27728,
                            Notes = "Test Notes",
                            Result = "WIN",
                            RiskReward = "1:3",
                            StopLoss = 1.2549999999999999,
                            TakeProfit = 1.3200000000000001,
                            Ticker = "USD/CAD",
                            Type = 0
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
