using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Restarted.System.Application.Common.Behaviours;
using System.Reflection;

namespace Restarted.System.Application
{
    public static class DependencyInjectionRegister
    {

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //Add all mediatrs from assembly
            services.AddMediatR(cfg =>
            {

                cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
                //cfg.AddOpenBehavior(typeof(IndempotentPipelineBehaviour<,>));
            });
            services.AddScoped(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            services.AddScoped(
               typeof(IPipelineBehavior<,>),
               typeof(CachingBehavior<,>));

         

            //Add all Validators from assembly
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            return services;
        }


    }
}
