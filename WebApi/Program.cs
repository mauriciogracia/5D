using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Persistance;

namespace WebApi
{
    public class Program
    {
        private const string API_NAME = "PERMISSIONS_API";
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            bool isDev = builder.Environment.IsDevelopment();

            // Register the ApiDbContext as a scoped service
            builder.Services.AddDbContext<ApiDbContext>(options =>
            {
                var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                options.UseSqlServer(connectionString);
            });

            // Dependency Injection
            builder.Services.AddTransient<IPersistPermissions, PersistPermissionsEF>();
            builder.Services.AddTransient<IPersistPermissionTypes, PersistPermissionTypesEF>();

            // Add services to the container.
            builder.Services.AddControllers();

            if (isDev)
            {
                // Configure Swagger when in development environment
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = API_NAME, Version = "v1" });
                });
            }

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (isDev)
            {
                // Enable Swagger UI
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", API_NAME);
                });
            }

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            
            app.MapControllers();

            app.Run();
        }

    }
}