using AuthService.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace AuthService
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            builder.Services.AddDbContext<AetheraDbContext>(options =>
            {
                options.ConfigureWarnings(warnings => warnings.Log(CoreEventId.NavigationBaseIncludeIgnored));
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"),
                                  builder =>
                                  {
                                      builder.EnableRetryOnFailure(5, TimeSpan.FromSeconds(10), null);
                                      builder.CommandTimeout(60);
                                  });
                options.UseLoggerFactory(LoggerFactory.Create(builder => builder.AddFilter((category, level) => level >= LogLevel.Warning)));
            }, ServiceLifetime.Scoped);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapGroup("/api").MapControllers();

            app.Run();
        }
    }
}
