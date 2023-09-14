﻿// <auto-generated />
using System;
using JackStockApi.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace JackStockApi.Migrations
{
    [DbContext(typeof(StockContext))]
    [Migration("20230914082954_Stock_LastUpdateDate")]
    partial class Stock_LastUpdateDate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("JackStockApi.Domain.Stock", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("date");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(25)
                        .HasColumnType("nvarchar(25)");

                    b.Property<int>("StockMarketTypeId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StockMarketTypeId");

                    b.ToTable("Stock", (string)null);
                });

            modelBuilder.Entity("JackStockApi.Domain.StockDayHistory", b =>
                {
                    b.Property<int>("StockId")
                        .HasColumnType("int");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("date");

                    b.Property<double>("Change")
                        .HasColumnType("float");

                    b.Property<double>("ClosingPrice")
                        .HasColumnType("float");

                    b.Property<double>("HighestPrice")
                        .HasColumnType("float");

                    b.Property<double>("LowestPrice")
                        .HasColumnType("float");

                    b.Property<double>("OpeningPrice")
                        .HasColumnType("float");

                    b.Property<long>("TradeValue")
                        .HasColumnType("bigint");

                    b.Property<long>("TradeVolumn")
                        .HasColumnType("bigint");

                    b.Property<long>("Transaction")
                        .HasColumnType("bigint");

                    b.HasKey("StockId", "DateTime");

                    b.ToTable("StockDayHistory", (string)null);
                });

            modelBuilder.Entity("JackStockApi.Domain.StockMarketType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.HasKey("Id");

                    b.ToTable("StockMarketType", (string)null);
                });

            modelBuilder.Entity("JackStockApi.Domain.Stock", b =>
                {
                    b.HasOne("JackStockApi.Domain.StockMarketType", "StockMarketType")
                        .WithMany("Stocks")
                        .HasForeignKey("StockMarketTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StockMarketType");
                });

            modelBuilder.Entity("JackStockApi.Domain.StockDayHistory", b =>
                {
                    b.HasOne("JackStockApi.Domain.Stock", "Stock")
                        .WithMany("StockHistories")
                        .HasForeignKey("StockId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Stock");
                });

            modelBuilder.Entity("JackStockApi.Domain.Stock", b =>
                {
                    b.Navigation("StockHistories");
                });

            modelBuilder.Entity("JackStockApi.Domain.StockMarketType", b =>
                {
                    b.Navigation("Stocks");
                });
#pragma warning restore 612, 618
        }
    }
}
