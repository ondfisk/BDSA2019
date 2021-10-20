using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using BDSA2019.Lecture10.Entities;
using BDSA2019.Lecture10.Models;
using Microsoft.OpenApi.Models;
using BDSA2019.Lecture10.Web.Models;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace BDSA2019.Lecture10.Web
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
            }
            else
            {
                app.UseHttpsRedirection();
            }

            
            app.UseRouting();

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
