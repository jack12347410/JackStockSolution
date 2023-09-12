
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
               
            }

            app.UseSwagger();
            app.UseSwaggerUI(c=>c.SwaggerEndpoint("/swagger/v1/swagger.json", "JackStockApi v1"));

            app.UseRouting();

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

        /// <summary>
        /// 取得mssql 連線字串
        /// </summary>
        /// <returns></returns>
        private string MssqlConnStr(WebApplicationBuilder builder)
        {
            string? server = builder.Configuration["DatabaseServer"];
            string? database = builder.Configuration["DatabaseName"];
            string? user = builder.Configuration["DatabaseUser"];
            string? password = builder.Configuration["DatabasePassword"];
            string? trust = builder.Configuration["TrustServerCertificate"];
            string connectionString = string.Format("Server={0};Database={1};TrustServerCertificate={2};User={3};Password={4};", 
                server, database, trust, user, password);

            return connectionString;
        }
    }
}