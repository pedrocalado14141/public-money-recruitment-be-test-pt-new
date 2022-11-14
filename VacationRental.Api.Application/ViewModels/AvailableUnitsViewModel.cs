using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Api.Application.ViewModels
{
    public class AvailableUnitsViewModel
    {
        public DateTime lastDayBooked { get; set; }
        public List<DateTime> PrepTimeDates { get; set; }
        public int Unit { get; set; }
        public bool Isblocked { get; set; }
    }
}
