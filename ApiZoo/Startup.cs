using ApiZoo.Models;
using AutoMapper;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNet.OData.Formatter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ZooDto;
using ZooService;
using ZooService.Automapper;
using ZooService.Business;
using ZooService.Contracts.Manager;
using ZooService.Contracts.Repository;
using ZooService.Models;
using ZooService.Repository;

namespace ApiZoo
{
    /// <summary>
    /// Startup
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// The env
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
            _env = env;
            Configuration = configuration;
        }



        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services">The services.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var connPayRol = _env.IsDevelopment() ? Configuration.GetConnectionString("ENVIRONMENT_ZOO_CONECTION") : Environment.GetEnvironmentVariable("ENVIRONMENT_ZOO_CONECTION");
            services.AddCors(c =>
            {
                c.AddPolicy("AllowOrigin", options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            });

            services.AddControllers().AddNewtonsoftJson();

            services.AddOData();

            SetOutputFormatters(services);

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

            services.AddDbContext<ZooContext>(options => options.UseNpgsql(connPayRol));
            services.AddAutoMapper(typeof(ZooProfileMap));

            #region Scope manager
            services.AddScoped<IAnimalManager, AnimalManager>();
            #endregion

            #region Scope repository
            services.AddScoped<IGetData<Animal>, GetRepository<Animal>>();
            services.AddScoped<ICrudBasic<Animal>, CrudRepository<Animal>>();
            #endregion

            var secret = Encoding.ASCII.GetBytes(appSettings.Secret);

            services.AddAuthorization(options =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(secret),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidIssuer = appSettings.Issuer,
                    ValidAudience = appSettings.Audience,
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddSwaggerGen(Swagger =>
            {
                Swagger.EnableAnnotations();
                Swagger.OperationFilter<ODataQueryOptionsFilter>();
                Swagger.AddServer(new OpenApiServer()
                {
                    Url = appSettings.UrlServerSwagger,
                    Description = "Local development server"
                });

                Swagger.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1.0.0",
                    Title = "Zoo Web Api",
                    Description = "It allows manage informarion of the Zoo.",
                    Contact = new OpenApiContact()
                    {
                        Name = "Ing. Victor A. Echeverria",
                        Email = "viktoralfa19@hotmail.com",
                        Url = new Uri("https://www.google.com/"),
                    }
                });

                Swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "JWT authorization header using Bearer schema",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });

                Swagger.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme
                    {
                      Reference = new OpenApiReference
                      {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                      }
                    },
                    new string[] { }
                    }
                });

                var basePath = _env.ContentRootPath;
                var xmlPath = Path.Combine(basePath, "ApiZoo.xml");
                var xmlDtoPath = Path.Combine(basePath, "ZooDto.xml");
                var xmlAuthPath = Path.Combine(basePath, "ZooService.xml");
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
                if (_env.IsDevelopment())
                {
                    url = "/swagger/v1/swagger.json";
                }
                else
                {
                    url = "../swagger/v1/swagger.json";
                }

                SwaggerUI.SwaggerEndpoint(url, "Zoo Management Service v1.0.0");
            });

            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.EnableDependencyInjection();
                endpoints.Expand().Select().Filter().Count().MaxTop(1000);
            });

            #region Migartion Auto
            // Create database on startup  
            //using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            //{
            //    serviceScope.ServiceProvider.GetService<ZooContext>().Database.Migrate();
            //}
            #endregion
        }

        /// <summary>
        /// Sets the output formatters.
        /// </summary>
        /// <param name="services">The services.</param>
        private static void SetOutputFormatters(IServiceCollection services)
        {
            services.AddMvcCore(op =>
            {
                IEnumerable<ODataOutputFormatter> outFormatters = op.OutputFormatters.OfType<ODataOutputFormatter>().Where(fmt => fmt.SupportedMediaTypes.Count() == 0);
                foreach (var outFormatter in outFormatters)
                {
                    outFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
                IEnumerable<ODataInputFormatter> inFormatters = op.InputFormatters.OfType<ODataInputFormatter>().Where(fmt => fmt.SupportedMediaTypes.Count() == 0);
                foreach (var inFormatter in inFormatters)
                {
                    inFormatter.SupportedMediaTypes.Add(new MediaTypeHeaderValue("application/odata"));
                }
            });
        }
    }
}
