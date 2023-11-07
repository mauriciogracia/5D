using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Persistance;
using WebApi.Persistance.CQRS;
using WebApi.Persistance.EntityFramework;

namespace WebApi
{
    public class Program
    {
        private static void PeristanceStrategy(WebApplicationBuilder builder)
        {
            Config.PersistanceStrategy = builder.Configuration.GetValue<string>("PersistanceStrategy");

            if (Config.PersistanceStrategy.Equals("PS_CACHE"))
            {
                ConfigureCacheInjection(builder);
            }
            else if (Config.PersistanceStrategy.Equals("PS_DATABASE"))
            {
                ConfigureDatabaseInjection(builder);
            }
            else if (Config.PersistanceStrategy.Equals("PS_CQRS"))
            {
                ConfigureDatabaseInjection(builder);
                ConfigureCacheInjection(builder);
                ConfigureCQRSInjection(builder);
            }
        }

        private static void ConfigureDatabaseInjection(WebApplicationBuilder builder)
        {
            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            AddDatabaseServices(builder.Services, connectionString);
        }

        private static void ConfigureCacheInjection(WebApplicationBuilder builder)
        {
            var elasticsearchUri = builder.Configuration.GetConnectionString("ElasticURI");
            AddCacheServices(builder.Services, elasticsearchUri);
        }

        private static void ConfigureCQRSInjection(WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IRepository<Permission>>(provider =>
            {
                var databaseRepository = provider.GetRequiredService<IRepository<Permission>>();
                var cacheRepository = provider.GetRequiredService<IRepository<Permission>>();
                return new RepositoryCQRS(databaseRepository, cacheRepository);
            });
        }

        private static void AddDatabaseServices(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApiDbContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddScoped<IRepository<Permission>, PermissionRepository>();
            services.AddScoped<IRepository<PermissionType>, PermissionTypeRepository>();
        }

        private static void AddCacheServices(IServiceCollection services, string elasticsearchUri)
        {
            services.AddScoped<IRepository<Permission>>(provider =>
            {
                return new PermissionElastic(elasticsearchUri);
            });
            services.AddScoped<IRepository<PermissionType>>(provider =>
            {
                return new PermissionTypeElastic(elasticsearchUri);
            });
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