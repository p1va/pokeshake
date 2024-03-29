using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using PokeShake.Dto;
using PokeShake.Services.FunTranslations;
using PokeShake.Services.FunTranslations.Contracts;
using PokeShake.Services.PokeApi;
using PokeShake.Services.PokeApi.Contracts;
using PokeShake.Services.PokemonShakespeareanDescription;
using PokeShake.Services.PokemonShakespeareanDescription.Contracts;
using PokeShake.Services.PokemonShakespeareanDescription.Exceptions;
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

        /// <summary>
        /// Configures the services.
        /// This method gets called by the runtime. Use this method to add services to the container.
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

            // Add in memory cache
            services.AddMemoryCache(options => 
            {
                // TODO: Set a cache size limit here
            });

            // Retrieve configuration and inject services options
            services.Configure<HttpFunTranslationsServiceOptions>(Configuration.GetSection("FunTranslations"));
            services.Configure<HttpPokeApiServiceOptions>(Configuration.GetSection("PokeApi"));

            // Register services implementation
            services.AddHttpClient<IPokeApiService, HttpPokeApiService>();
            services.AddHttpClient<IFunTranslationsService, HttpFunTranslationsService>();
            services.AddScoped<IPokemonShakespeareanDescriptionService, PokemonShakespeareanDescriptionService>();
        }

        /// <summary>
        /// Configures the specified application.
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            // Add the global exception handler
            app.UseExceptionHandler(a => a.Run(async context =>
            {
                // Retrieve the exception handler
                var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();

                // Extract the exception
                var exception = exceptionHandlerPathFeature.Error;

                // Declare response string
                string response = null;

                // Declare status code
                int statusCode = 0;

                switch (exception)
                {
                    case PokemonInvalidArgsException invalidException:

                        // Set status code to 400
                        statusCode = (int)HttpStatusCode.BadRequest;

                        // Create a new response
                        response = JsonConvert.SerializeObject(new PokemonBadRequestResponse
                        { 
                            ErrorCode = ErrorCodes.Pokemon.InvalidName, 
                            ErrorMessage = invalidException.Message
                        });

                        break;

                    case PokemonNotFoundException notFoundException:

                        // Set status code to 404
                        statusCode = (int)HttpStatusCode.NotFound;

                        // Create a new response
                        response = JsonConvert.SerializeObject(new PokemonNotFoundResponse
                        {
                            ErrorCode = ErrorCodes.Pokemon.NotFound,
                            ErrorMessage = notFoundException.Message
                        });

                        break;

                    default:

                        // Set status code to 400
                        statusCode = (int)HttpStatusCode.InternalServerError;

                        // Create a new response
                        response = JsonConvert.SerializeObject(new InternalServerErrorResponse
                        {
                            ErrorCode = ErrorCodes.Pokemon.InternalServerError,
                            ErrorMessage = exception.Message
                        });

                        break;
                }

                context.Response.StatusCode = statusCode;
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(response);
            }));

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
