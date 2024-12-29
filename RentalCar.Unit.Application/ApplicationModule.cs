using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;
using RentalCar.Unit.Application.Handlers;
using RentalCar.Unit.Application.Validators;

namespace RentalCar.Unit.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services
                .AddFluentValidation()
                .AddHandlers()
                .AddBackgroundServices();
            return services;
        }


        private static IServiceCollection AddFluentValidation(this IServiceCollection services)
        {
            services.AddFluentValidationAutoValidation()
                .AddValidatorsFromAssemblyContaining<CreateUnitValidator>();

            return services;
        }

        private static IServiceCollection AddHandlers(this IServiceCollection services)
        {
            services.AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateUnitHandler>());

            return services;
        }
        
        private static IServiceCollection AddBackgroundServices(this IServiceCollection services)
        {
            
            return services;
        }

    }
}
