using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SecurityService;
using SecurityService.Models;
using System;
using Microsoft.EntityFrameworkCore;
using SecurityService.Contracts;
using SecurityService.Business;
using Microsoft.OpenApi.Models;
using System.IO;
using SecurityDto;
using Microsoft.AspNetCore.Rewrite;

/// <summary>
/// Security
/// </summary>
namespace ApiSecurity
{
    /// <summary>
    /// Startup Class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The environment
        /// </summary>
        private readonly IWebHostEnvironment _env;
        /// <summary>
        /// Gets the configuration.
        /// </summary>
        /// <value>
        /// The configuration.
        /// </value>
        public IConfiguration Configuration { get; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="configuration">The configuration.</param>
        /// <param name="env">The env.</param>
        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var conUser = _env.IsDevelopment() ? Configuration.GetConnectionString("ENVIRONMENT_USER_CONECTION") : Environment.GetEnvironmentVariable("ENVIRONMENT_USER_CONECTION");
            
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers();

            AppSettingsDto appSettings;
            if (_env.IsDevelopment())
            {
                var appSettingsSection = Configuration.GetSection("AppSettings");
                services.Configure<AppSettingsDto>(appSettingsSection);
                appSettings = appSettingsSection.Get<AppSettingsDto>();
            }
            else
            {
                services.Configure<AppSettingsDto>(Configuration);
                appSettings = Configuration.Get<AppSettingsDto>();
            }

            services.AddIdentity<ApplicationUser, ApplicationRole>()
            .AddEntityFrameworkStores<SecurityContext>()
            .AddDefaultTokenProviders();

            services.AddDbContext<SecurityContext>(options => options.UseNpgsql(conUser));

            services.AddScoped<IApplicationUserManager, ApplicationUserManager>();

            services.AddSwaggerGen(Swagger =>
            {
                Swagger.EnableAnnotations();
                Swagger.AddServer(new OpenApiServer()
                {
                    Url = appSettings.UrlServerSwagger,
                    Description = "Local development server"
                });
                Swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "Web Api Authentication",
                    Description = "JWT token authentication API.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ing. Victor A. Echeverria",
                        Email = "viktoralfa19@hotmail.com",
                        Url = new System.Uri("https://www.google.com/"),
                    }
                });
                var basePath = _env.ContentRootPath;
                var xmlPath = Path.Combine(basePath, "ApiSecurity.xml");
                var xmlDtoPath = Path.Combine(basePath, "SecurityDto.xml");
                var xmlAuthPath = Path.Combine(basePath, "SecurityService.xml");
                Swagger.IncludeXmlComments(xmlPath);
                Swagger.IncludeXmlComments(xmlDtoPath);
                Swagger.IncludeXmlComments(xmlAuthPath);
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app">The application.</param>
        /// <param name="env">The env.</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            string url = string.Empty;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("AllowOrigin");

            app.UseAuthorization();

            app.UseSwagger();
            app.UseSwaggerUI(SwaggerUI => {
                if (env.IsDevelopment())
                {
                    url = "/swagger/v1/swagger.json";
                }
                else
                {
                    url = "../swagger/v1/swagger.json";
                }

                SwaggerUI.SwaggerEndpoint(url, "Generate Token Services v1.0");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            #region Migartion Auto
            // Create database on startup  
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetService<SecurityContext>().Database.Migrate();
            //}
            #endregion
        }
    }
}
