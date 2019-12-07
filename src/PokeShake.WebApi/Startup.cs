using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using PokeShake.WebApi.Schemas;

namespace PokeShake.WebApi
{
    /// <summary>
    /// The startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        /// <summary>
        /// Configures the services.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddControllers()
                .AddNewtonsoftJson();

            // Add swagger gen to the services collection
            services.AddSwaggerGen(options =>
            {
                // Add some basing info
                // TODO: Add more
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "PokeShake API",
                    Version = "v1"
                });

                // Add schema filters
                options.AddAppSchemaFilters();

                // Declare the XML documentation file
                var xmlDocumentationFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

                // Combine and obtain the XML documentation file path
                var xmlDocumentationFilePath = Path.Combine(AppContext.BaseDirectory, xmlDocumentationFile);

                // Set the XML documentation file location so that Swagger can include it in the UI
                options.IncludeXmlComments(xmlDocumentationFilePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// <summary>
        /// Configures the specified application.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve the generated Swagger JSON
            app.UseSwagger();

            // Enable middleware to serve the Swagger UI
            app.UseSwaggerUI(c =>
            {
                // Configure the UI with the endpoint where Swagger file is located
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PokeShake V1");

                // Set swaggeer rout to /
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
