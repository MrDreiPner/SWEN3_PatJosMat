/*
 * Paperless Rest Server
 *
 * No description provided (generated by Openapi Generator https://github.com/openapitools/openapi-generator)
 *
 * The version of the OpenAPI document: 1.0
 * 
 * Generated by: https://openapi-generator.tech
 */

using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using AutoMapper;
using NPaperless.REST.Authentication;
using NPaperless.REST.Filters;
using NPaperless.REST.OpenApi;
using NPaperless.REST.Formatters;
using FluentValidation;
using NPaperless.REST.DTOs;
using NPaperless.BusinessLogic.Entities;
using NPaperless.BusinessLogic.Validators;
using NPaperless.DataAccess.Interfaces;
using NPaperless.DataAccess.SQL;
using NPaperless.BusinessLogic.Services;
using NPaperless.BusinessLogic.Interfaces;
using log4net;
using log4net.Config;
using NPaperless.BusinessLogic;
using Microsoft.EntityFrameworkCore;
using Minio;
using System.Threading.Tasks;

namespace NPaperless.REST
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(Startup));
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// The application configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            _logger.Info("START: Configure Services ...");

            _logger.Info("Adding CORS services.");
            services.AddCors();

            _logger.Info("Adding business layer services.");
            services.AddBusinessLayer();

            _logger.Info("Adding validators.");
            services.AddScoped<IValidator<TagBL>, ValidatorTag>();
            services.AddScoped<IValidator<CorrespondentBL>, ValidatorCorrespondent>();
            services.AddScoped<IValidator<DocumentBL>, ValidatorDocument>();

            _logger.Info("Adding database context.");
            services.AddDbContext<NPaperlessDbContext>(options => options.UseNpgsql("Host=npaperless-db;Username=dev;Password=dev;Database=npaperless"));

            _logger.Info("Adding repositories and services.");
            services.AddScoped<ITagDALRepository, TagDALRepository>();
            services.AddScoped<IDocumentDALRepository, DocumentDALRepository>();

            services.AddScoped<ITagService, TagService>();
            services.AddScoped<IDocumentService, DocumentService>();


            // Add framework services.
            _logger.Info("Configuring framework services.");
            services.AddSingleton<IMinioClient>(provider =>
            {
                _logger.Info("Doing mario stuff");

                var minioClient = new MinioClient()
                .WithEndpoint("npaperless-minio:9000")
                .WithCredentials("npaperless", "npaperless")
                .Build();

                EnsureBucketExistsAsync(minioClient, "npaperless-bucket").Wait();

                return minioClient;
            });

            services
                // Don't need the full MVC stack for an API, see https://andrewlock.net/comparing-startup-between-the-asp-net-core-3-templates/
                .AddControllers(options => {
                    options.InputFormatters.Insert(0, new InputFormatterStream());
                })
                .AddNewtonsoftJson(opts =>
                {
                    opts.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                    opts.SerializerSettings.Converters.Add(new StringEnumConverter
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    });
                });
            _logger.Info("Configuring Swagger.");
            services
                .AddSwaggerGen(c =>
                {
                    c.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);
                    
                    c.SwaggerDoc("1.0", new OpenApiInfo
                    {
                        Title = "Paperless Rest Server",
                        Description = "Paperless Rest Server (ASP.NET Core 6.0)",
                        TermsOfService = new Uri("https://github.com/openapitools/openapi-generator"),
                        Contact = new OpenApiContact
                        {
                            Name = "OpenAPI-Generator Contributors",
                            Url = new Uri("https://github.com/openapitools/openapi-generator"),
                            Email = ""
                        },
                        License = new OpenApiLicense
                        {
                            Name = "NoLicense",
                            Url = new Uri("http://localhost")
                        },
                        Version = "1.0",
                    });
                    c.CustomSchemaIds(type => type.FriendlyId(true));
                    c.IncludeXmlComments($"{AppContext.BaseDirectory}{Path.DirectorySeparatorChar}{Assembly.GetEntryAssembly().GetName().Name}.xml");

                    // Include DataAnnotation attributes on Controller Action parameters as OpenAPI validation rules (e.g required, pattern, ..)
                    // Use [ValidateModelState] on Actions to actually validate it in C# as well!
                    c.OperationFilter<GeneratePathParamsValidationFilter>();
                });
            _logger.Info("Adding Newtonsoft.Json support for Swagger.");
            services
                    .AddSwaggerGenNewtonsoftSupport();
            _logger.Info("END: Configure Services.");
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            _logger.Info("START: Configure Application ...");
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(origin => true) // allow any origin
                .AllowCredentials()); // allow credentials
            app.UseSwagger(c =>
                {
                    c.RouteTemplate = "openapi/{documentName}/openapi.json";
                })
                .UseSwaggerUI(c =>
                {
                    // set route prefix to openapi, e.g. http://localhost:8080/openapi/index.html
                    c.RoutePrefix = "openapi";
                    //TODO: Either use the SwaggerGen generated OpenAPI contract (generated from C# classes)
                    c.SwaggerEndpoint("/openapi/1.0/openapi.json", "Paperless Rest Server");

                    //TODO: Or alternatively use the original OpenAPI contract that's included in the static files
                    // c.SwaggerEndpoint("/openapi-original.json", "Paperless Rest Server Original");
                });
            _logger.Info("Configuring Routing and Endpoints.");
            app.UseRouting();
            app.UseEndpoints(endpoints =>
                {
                    endpoints.MapControllers();
                });
            _logger.Info("END: Configure Application.");
        }

        private async Task EnsureBucketExistsAsync(IMinioClient minioClient, string bucketName)
        {
            try
            {
                _logger.Info("Looking for my bucket");

                // Check if the bucket already exists
                var bucketExistsArgs = new BucketExistsArgs().WithBucket(bucketName);
                bool bucketExists = await minioClient.BucketExistsAsync(bucketExistsArgs);

                _logger.Info("Where is bucket?");

                // If the bucket doesn't exist, create it
                if (!bucketExists)
                {
                    _logger.Info("Bucket not exists");
                    var makeBucketArgs = new MakeBucketArgs().WithBucket(bucketName);
                    await minioClient.MakeBucketAsync(makeBucketArgs);
                }
            }
            catch (Exception ex)
            {
                _logger.Info("Upsi Pupsi");
            }
        }
    }
}
