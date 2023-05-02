using Application.Interfaces;
using Application.ViewModels.Customer;
using Application.ViewModels.Order;
using Application.ViewModels.Product;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Diagnostics;
using System.Text;
using WebAPI.Middlewares;
using WebAPI.Services;
using WebAPI.Validations;

namespace WebAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebAPIService(this IServiceCollection services,
                                                          string JWTKey,
                                                          string JWTIssuer,
                                                          string JWTAudience,
                                                          string OutLookClient)
        {
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            services.AddHealthChecks();
            services.AddSingleton<GlobalExceptionMiddleware>();
            services.AddSingleton<PerformanceMiddleware>();
            services.AddSingleton<Stopwatch>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddHttpContextAccessor();
            services.AddFluentValidationAutoValidation();
            services.AddFluentValidationClientsideAdapters();

            #region Validation
            // customer
            services.AddScoped<IValidator<CreateCustomer>, CreateCustomerValidation>();
            services.AddScoped<IValidator<UpdateCustomer>, UpdateCustomerValidation>();

            // order
            services.AddScoped<IValidator<CreateOrder>, CreateOrderValidation>();
            services.AddScoped<IValidator<UpdateOrder>, UpdateOrderValidation>();

            // product
            services.AddScoped<IValidator<CreateProduct>, CreateProductValidation>();
            services.AddScoped<IValidator<UpdateProduct>, UpdateProductValidation>();

            #endregion
            // JWT
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(o =>
            {
                o.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = JWTIssuer,
                    ValidAudience = JWTAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JWTKey)),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true
                };
            });

            services.AddAuthorization();


            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Ecommerce",
                    Version = "v1",
                    Description = "API for smail ecommerce project",
                    Contact = new OpenApiContact
                    {
                        Url = new Uri("https://google.com")
                    }
                });

                // Add JWT authentication support in Swagger
                var securityScheme = new OpenApiSecurityScheme

                {
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                options.AddSecurityDefinition("Bearer", securityScheme);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {
                        securityScheme, new[] { "Bearer" }
                    }
                };

                options.AddSecurityRequirement(securityRequirement);
            });
            // Http client 
            services.AddHttpClient("OutLook", httpClient =>
            {
                var baseAddress = OutLookClient;
                httpClient.BaseAddress = new Uri(baseAddress);
            });
            return services;
        }
    }
}
