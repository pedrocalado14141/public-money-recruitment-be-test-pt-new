using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Api.Application.ViewModels
{
    public class UnitsToCleanViewModel
    {
        public DateTime Date { get; set; }
        public int Unit { get; set; }
        public int BookingId { get; set; }
        public bool Isblocked { get; set; }
    }
}
