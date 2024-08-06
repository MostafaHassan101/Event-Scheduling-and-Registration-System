using AutoMapper.Internal;
using EventSystem.Application.Common.Interfaces;
using EventSystem.Infrastructure.Identity;
using EventSystem.Infrastructure.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EventSystem.Infrastructure.Services;
using EventSystem.Domain.Repositories;
using EventSystem.Infrastructure.Repository;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace EventSystem.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryDatabase"))
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseInMemoryDatabase("EventSystemDb"));
        }
        else
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));
        }



        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        services.AddScoped<ApplicationDbContextInitialiser>();

        /// Identity Service Configuration
        services.AddDefaultIdentity<ApplicationUser>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 8;
            options.Password.RequiredUniqueChars = 0;
        }
        )
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

        /// Identity Service
        services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        services.AddScoped<IIdentityService, IdentityService>();

        services.AddScoped<IEventRepository, EventRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IEventRegistrationService, EventRegistrationService>();
        services.AddScoped<IUserRegistrationService, UserRegistrationService>();
        services.AddScoped<ITokenService<ApplicationUser>, TokenService>();

        services.AddAutoMapper(cfg =>
            {
                cfg.Internal().MethodMappingEnabled = false;
                cfg.AddProfile(new Mapping.ProfileMapping());
            });

        services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
                );

        return services;
    }
}
