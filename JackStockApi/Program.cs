
using JackStockApi.Data;
using JackStockApi.Domain;
using JackStockApi.Repositorys;
using JackStockApi.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace JackStockApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<StockContext>(
                options => options.UseSqlServer(builder.Configuration.GetConnectionString("StockDBConnection")));
            builder.Services.AddScoped<StockRepo>();
            builder.Services.AddScoped<StockService>();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Jack Stock API",
                    Description = "Jack Stock API",
                    Contact = new OpenApiContact
                    {
                        Name = "Jack",
                        Email = string.Empty,
                        Url = new Uri("https://github.com/jack12347410"),
                    }
                });
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();

            app.MapControllers();

            CreateStockSeed(app);

            app.Run();
        }

        /// <summary>
        /// 建立預設資料
        /// </summary>
        /// <param name="host"></param>
        private static void CreateStockSeed(IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<StockContext>();
                    StockSeed.SeedAsync(context).Wait();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occured while seeding the database");
                }
            }
        }
    }
}