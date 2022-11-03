using System.Collections.Generic;
using VacationRental.Api.Application.ViewModels;

namespace VacationRental.Api.Application.Models
{
    public class CalendarViewModel
    {
        public int RentalId { get; set; }
        public List<CalendarDateViewModel> Dates { get; set; }
    }
}
