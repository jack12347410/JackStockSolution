using JackStockApi.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JackStockApi.Data
{
    public class StockContext : DbContext
    {
        public DbSet<Stock> Stock { get; set; }
        public DbSet<StockDayHistory> StockDayHistory { get; set; }
        public DbSet<StockMarketType> StockMarketType { get; set; }

        public StockContext(DbContextOptions options): base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //增加 sql index...

            modelBuilder.Entity<Stock>(x =>
            {
                x.ToTable("Stock");
                x.HasKey(c => c.Id);

                x.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(25);

                x.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(10);

                x.Property(c => c.StockMarketTypeId)
                .IsRequired();

                x.HasOne(d => d.StockMarketType)
                .WithMany(p => p.Stocks)
                .HasForeignKey(d => d.StockMarketTypeId);

                x.Property(c => c.LastUpdateDate)
                .IsRequired()
                .HasColumnType("date")
                .HasDefaultValue(new DateTime(2010, 1, 1));

            });

            modelBuilder.Entity<StockMarketType>(x =>
            {
                x.ToTable("StockMarketType");
                x.HasKey(c => c.Id);

                x.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(10);

                x.Property(c => c.Code)
                .IsRequired()
                .HasMaxLength(10);
            });

            modelBuilder.Entity<StockDayHistory>(x =>
            {
                x.ToTable("StockDayHistory");
                x.HasKey(c => new { c.StockId, c.DateTime });

                x.HasOne(d => d.Stock)
                .WithMany(p => p.StockHistories)
                .HasForeignKey(d => d.StockId);

                x.Property(c => c.DateTime)
                .HasColumnType("date");

            });
        }
    }
}
