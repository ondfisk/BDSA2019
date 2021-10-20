using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BDSA2019.Lecture11.Entities;
using BDSA2019.Lecture11.Models;
using Microsoft.OpenApi.Models;
using BDSA2019.Lecture11.Web.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Logging;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BDSA2019.Lecture11.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddHttpContextAccessor();
            services.Configure<JwtBearerOptions>(AzureADDefaults.JwtBearerAuthenticationScheme, options =>
            {
                // The valid audiences are both the clientId (options.Audience) and api://{clientId}
                options.TokenValidationParameters.ValidAudiences = new[]
                {
                    options.Audience,
                    $"api://{options.Audience}"
                };
            });

            services.AddControllers();
            services.AddRouting(options => options.LowercaseUrls = true);

            services.AddDbContext<SuperheroContext>(o => o.UseSqlServer(Configuration.GetConnectionString("SuperheroContext")));
            services.AddScoped<ISuperheroContext, SuperheroContext>();
            services.AddScoped<ISuperheroRepository, SuperheroRepository>();
            services.AddScoped<IBlobManager, BlobManager>();
            services.AddScoped(o =>
            {
                var blobServiceClient = new BlobServiceClient(Configuration.GetConnectionString("BlobStorage"));
                var containerClient = blobServiceClient.GetBlobContainerClient("superheroes");
                containerClient.CreateIfNotExists(PublicAccessType.Blob);
                return containerClient;
            });

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // Since IdentityModel version 5.2.1 (or since Microsoft.AspNetCore.Authentication.JwtBearer version 2.2.0),
                // PII hiding in log files is enabled by default for GDPR concerns.
                // For debugging/development purposes, one can enable additional detail in exceptions by setting IdentityModelEventSource.ShowPII to true.
                IdentityModelEventSource.ShowPII = true;
            }
            else
            {
                app.UseHttpsRedirection();
                app.UseHsts();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
            // specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<SuperheroContext>();
                context.Database.Migrate();
            }
        }
    }
}
