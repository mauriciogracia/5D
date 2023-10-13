using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Persistance;
using WebApi.Persistance.EntityFramework;

namespace WebApi
{
    public class Program
    {
        static bool UseInMemoryDB = false;
        private const string API_NAME = "PERMISSIONS_API";

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            bool isDev = builder.Environment.IsDevelopment();

            // Register the ApiDbContext as a scoped service
            if (UseInMemoryDB)
            {
                builder.Services.AddDbContext<ApiDbContext>(options =>
        options.UseInMemoryDatabase(databaseName: "InMemoryDb"));
            }
            else
            {
                builder.Services.AddDbContext<ApiDbContext>(options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                });
            }

            // Dependency Injection
            builder.Services.AddTransient<IRepository<Permission>, PermissionRepository>();
            builder.Services.AddTransient<IRepository<PermissionType>, PermissionTypeRepository >();

            // Add services to the container.
            builder.Services.AddControllers();

            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOriginPolicy", builder =>
                {
                    builder
                        .AllowAnyOrigin()     // Allow requests from any origin
                        .AllowAnyMethod()     // Allow any HTTP method
                        .AllowAnyHeader();    // Allow any HTTP headers
                });
            });

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

            // Enable CORS
            app.UseCors("AllowAnyOriginPolicy");

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }

}