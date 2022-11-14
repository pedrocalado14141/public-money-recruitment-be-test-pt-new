﻿using System;
using System.Collections.Generic;

namespace VacationRental.Api.Application.ViewModels
{
    public class CalendarDateViewModel
    {
        public DateTime Date { get; set; }
        public List<CalendarBookingViewModel> Bookings { get; set; }
        public List<UnitViewModel> PreparationTimes { get; set; }
    }
}
