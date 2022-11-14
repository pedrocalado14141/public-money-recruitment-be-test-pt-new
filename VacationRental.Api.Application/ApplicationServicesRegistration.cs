using Microsoft.Extensions.DependencyInjection;
using VacationRental.Api.Application.Interfaces;
using VacationRental.Api.Application.Services;

namespace VacationRental.Api.Application
{
    public static class ApplicationServicesRegistration
    {
        public static void ConfigureApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IRentalService, RentalService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped<ICalendarService, CalendarService>();
        }
    }
}
