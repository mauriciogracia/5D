using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Persistance;
using WebApi.Persistance.EntityFramework;

namespace WebApi
{
    public class Program
    {
        private static void PeristanceStrategy(WebApplicationBuilder builder)
        {
            Config.UseMemoryDB = builder.Configuration.GetValue<bool>("UseMemoryDB");

            if (Config.UseMemoryDB)
            {
                /* USE InMemoryDb 
                builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase(databaseName: "InMemoryDb"));*/

                var elasticsearchUri = builder.Configuration.GetConnectionString("ElasticURI"); 

                // Dependency Injection
                builder.Services.AddScoped<IRepository<Permission>>(provider =>
                {
                    return new PermissionElastic(elasticsearchUri);
                });
                builder.Services.AddScoped<IRepository<PermissionType>>(provider =>
                {
                    return new PermissionTypeElastic(elasticsearchUri);
                });
            }
            else
            {
                builder.Services.AddDbContext<ApiDbContext>(options =>
                {
                    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
                    options.UseSqlServer(connectionString);
                });
                // Dependency Injection
                builder.Services.AddScoped<IRepository<Permission>, PermissionRepository>();
                builder.Services.AddScoped<IRepository<PermissionType>, PermissionTypeRepository>();
            }
        }

        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            bool isDev = builder.Environment.IsDevelopment();

            PeristanceStrategy(builder);

            // Add services to the container.
            builder.Services.AddControllers();

            SetupCORS(builder);

            if (isDev)
            {
                // Configure Swagger when in development environment
                builder.Services.AddEndpointsApiExplorer();
                builder.Services.AddSwaggerGen(c =>
                {
                    c.SwaggerDoc("v1", new OpenApiInfo { Title = Config.API_NAME, Version = "v1" });
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
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", Config.API_NAME);
                });
            }

            // Enable CORS
            app.UseCors(Config.CORS_POLICY_NAME);

            //app.UseHttpsRedirection();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        private static void SetupCORS(WebApplicationBuilder builder)
        {
            // Configure CORS
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(Config.CORS_POLICY_NAME, builder =>
                {
                    builder
                        .AllowAnyOrigin()     // Allow requests from any origin
                        .AllowAnyMethod()     // Allow any HTTP method
                        .AllowAnyHeader();    // Allow any HTTP headers
                });
            });
        }
    }

}