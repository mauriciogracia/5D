using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Models;
using WebApi.Persistance;
using WebApi.Persistance.EntityFramework;

namespace WebApi
{
    public class Program
    {
        private const string API_NAME = "PERMISSIONS_API";
        public const string CORS_POLICY_NAME = "AllowAnyOriginPolicy";

        private static void PeristanceStrategy(WebApplicationBuilder builder)
        {
            bool UseMemoryDB = builder.Configuration.GetValue<bool>("UseMemoryDB");

            if (UseMemoryDB)
            {
                /* USE InMemoryDb 
                builder.Services.AddDbContext<ApiDbContext>(options => options.UseInMemoryDatabase(databaseName: "InMemoryDb"));*/

                // Dependency Injection
                builder.Services.AddScoped<IRepository<Permission>>(provider =>
                {
                    var elasticsearchUri = builder.Configuration.GetConnectionString("ElasticURI"); // Get the Elasticsearch connection string from your configuration
                    return new PermissionElastic(elasticsearchUri);
                });

                //TODO add the 3 types of permission to ELASTIC SEARCH (similar to INSERT INTO)
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
            app.UseCors(Program.CORS_POLICY_NAME);

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
                options.AddPolicy(Program.CORS_POLICY_NAME, builder =>
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