using AutoMapper;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Restarted.System.Infrastructure.Persistence.Mapper;

namespace Restarted.System.Api.Administration
{
    public static class DependencyInjectionRegistration
    {
        public static IServiceCollection AddPresentation(this IServiceCollection services)
        {
            services.AddControllers();
            //services.AddSingleton<ProblemDetailsFactory, ApiProblemDetailsFactory>();
            services.AddAutoMapperProfiles();
            return services;
        }






        private static IServiceCollection AddAutoMapperProfiles(this IServiceCollection services)
        {// Auto Mapper Configurations

            List<Profile> profiles = new List<Profile>()
            {
                new  MapperProfile(),
               // new ContractMapperProfile()
            };
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfiles(profiles);

            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }

    }
}
