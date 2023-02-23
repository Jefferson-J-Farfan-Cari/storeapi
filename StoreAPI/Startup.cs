using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StoreAPI.Configuration;
using StoreAPI.Entities.Interface;
using StoreAPI.Infrastructure.Context;
using StoreAPI.Repository;

namespace StoreAPI;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    private IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();
        //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

        services.AddDbContext<StoreDb>(options =>
            options.UseNpgsql(Configuration.GetConnectionString("StoreDB")));

        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IStoreRepository, StoreRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<IConfigurationRepository, ConfigurationRepository>();

        services.AddMvcCore();

        services.AddSwaggerGen(options =>
        {
            var contact = new OpenApiContact
                {Name = SwaggerConfiguration.ContactName, Url = new Uri(SwaggerConfiguration.ContactUrl)};

            options.SwaggerDoc(SwaggerConfiguration.DocNameV1, new OpenApiInfo
            {
                Title = SwaggerConfiguration.DocInfoTitle,
                Version = SwaggerConfiguration.DocInfoVersionV1,
                Description = SwaggerConfiguration.DocInfoDescription,
                Contact = contact
            });

            options.OperationFilter<SwaggerFilter>();

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = @"JWT Authorization header Example: 'Bearer TOKEN'",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Authorization"
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "oauth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    Array.Empty<string>()
                }
            });
        });

        //https://stackoverflow.com/questions/53786977/signalr-core-2-2-cors-allowanyorigin-breaking-change
        services.AddCors(options =>
        {
            options.AddPolicy("access", access => access.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials()
                .SetIsOriginAllowed(_ => true));
        });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.UseCors("access");
        app.UsePathBase("/store");
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        app.UseSwagger();
        app.UseSwaggerUI(x =>
        {
            x.SwaggerEndpoint(SwaggerConfiguration.EndpointUrlV1, SwaggerConfiguration.EndpointDescriptionV1);
        });
        if (env.IsDevelopment())
            app.UseDeveloperExceptionPage();
        else
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        app.UseHttpsRedirection();
        // app.UseMvc();
    }
}