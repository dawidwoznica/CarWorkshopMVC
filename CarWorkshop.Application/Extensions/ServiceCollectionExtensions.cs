using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop.Commands.CreateCarWorkshopCommand;
using CarWorkshop.Application.CarWorkshop.Commands.EditCarWorkshopCommand;
using CarWorkshop.Application.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace CarWorkshop.Application.Extensions
{
    using FluentValidation;
    using FluentValidation.AspNetCore;

    public static class ServiceCollectionExtensions
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddScoped<IUserContext, UserContext>();

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(CreateCarWorkshopCommand).Assembly));


            services.AddScoped(provider => new MapperConfiguration(cfg =>
            {
                var scope = provider.CreateScope();
                var userContext = scope.ServiceProvider.GetRequiredService<IUserContext>();

                cfg.AddProfile(new CarWorkshopMappingProfile(userContext));
            }).CreateMapper());

            //services.AddAutoMapper(typeof(CarWorkshopMappingProfile));

            services.AddValidatorsFromAssemblyContaining<CreateCarWorkshopCommandValidator>()
                .AddValidatorsFromAssemblyContaining<EditCarWorkshopCommandValidator>()
                .AddFluentValidationAutoValidation()
                .AddFluentValidationClientsideAdapters();
        }
    }
}
