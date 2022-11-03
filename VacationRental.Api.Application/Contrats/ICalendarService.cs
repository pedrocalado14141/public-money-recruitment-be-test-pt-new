
using System;
using System.Threading.Tasks;
using VacationRental.Api.Application.Models;

namespace VacationRental.Api.Application.Interfaces
{
    public interface ICalendarService
    {
        Task<CalendarViewModel> GetAllAsync(int rentalId, DateTime start, int nights);
    }
}
