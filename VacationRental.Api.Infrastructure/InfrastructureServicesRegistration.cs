using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;
using VacationRental.Api.Domain.Repositories;

namespace VacationRental.Api.Domain
{
    public static class InfrastructureServicesRegistration
    {
        public static void ConfigureInfrastructureServices(this IServiceCollection services)
        {
            //Store Data
            services.AddSingleton<IDictionary<int, RentalViewModel>>(new Dictionary<int, RentalViewModel>());
            services.AddSingleton<IDictionary<int, BookingViewModel>>(new Dictionary<int, BookingViewModel>());

            // Services for Infrastructure
            services.AddScoped<IBookingRepository, BookingRepository>();
            services.AddScoped<IRentalRepository, RentalRepository>();

            //Unit of work for the repositories
            services.AddScoped<IUnitOfWork, UnitOfWork>();
        }
    }
}
