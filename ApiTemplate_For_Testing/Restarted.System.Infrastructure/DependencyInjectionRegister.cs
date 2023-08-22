using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Restarted.System.Contracts.Interfaces.Services;
using Restarted.System.Infrastructure.Authentication;
using Restarted.System.Infrastructure.Persistence.Entities;

using Restarted.System.Infrastructure.Services;

using System.Text;

namespace Restarted.System.Infrastructure
{
    public  static partial class DependencyInjectionRegister
    {
        private static partial IServiceCollection AddPersistanceAuto(this IServiceCollection services);

        public static IServiceCollection AddInfrastructure(
       this IServiceCollection services,
       IConfiguration configration)
        {
            services
                .AddAuth(configration)
                .AddPersistance(configration)
                // .AddAutoMapperProfiles()
                .AddServicesProvider()

                .AddCachingService(configration);

            return services;
        }



        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AwakenedSystemContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("Database")));

            // services.AddScoped<PublishDomainEventsInterceptor>();
         

            // Add Auto Generated Services
            AddPersistanceAuto(services);
            
            return services;
        }
        



        public static IServiceCollection AddAuth(
            this IServiceCollection services,
            IConfiguration configration)
        {
            var jwtSettings = new JwtSettings();
            configration.Bind(JwtSettings.SectionName, jwtSettings);

            services.AddSingleton(Options.Create(jwtSettings));
            //services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddAuthentication(defaultScheme: JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtSettings.Secret))
                });

            return services;
        }

        public static IServiceCollection AddCachingService(this IServiceCollection services,
           IConfiguration configration)
        {

            var cacheSettings = new CacheSettings();
            configration.Bind(CacheSettings.SectionName, cacheSettings);


            services.AddStackExchangeRedisCache(options =>
             {
                 options.Configuration = cacheSettings.ServerConnection;
                 //options.InstanceName = cacheSettings.InstanceName;
             });


            services.AddSingleton(Options.Create(cacheSettings));
            services.AddSingleton<ICacheService, CacheService>();
            return services;
        }

        public static IServiceCollection AddServicesProvider(this IServiceCollection services)
        {
            services.AddSingleton<ICacheService, CacheService>();
            services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
            services.AddSingleton<IEmailProvider, EmailProvider>();
            services.AddSingleton<ISMSProvider, SMSProvider>();

            return services;
        }
    }
}

