using System;
using System.Collections.Generic;
using System.Linq;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Application.ViewModels;

namespace VacationRental.Api.Application.Validations
{
    internal static class ValidationHelper
    {
        public static bool ValidateBooking(CalendarViewModel bookings, RentalBindingModel rentalBindingModel, int totalUnits)
        {
            List<ValidatePutRentalViewModel> validatePuts = new List<ValidatePutRentalViewModel>();
            List<ValidateAuxPut> validateAuxes = new List<ValidateAuxPut>();

            List<int> totalUnitsList = Enumerable.Range(1, totalUnits).ToList();
            foreach (var booking in bookings.Dates)
            {
                var getBookings = booking.Bookings;
                foreach (CalendarBookingViewModel book in getBookings)
                    validatePuts.Add(ToViewModel(book.Unit, booking.Date, "BookDay"));

                var getPrepTimes = booking.PreparationTimes;
                foreach (UnitViewModel prepTime in getPrepTimes)
                    validatePuts.Add(ToViewModel(prepTime.Unit, booking.Date, "CleanDay"));
            }

            var allPreparationItems = validatePuts.Where(r => r.IsCleanDay);

            foreach (var item in allPreparationItems)
            {
                int countOverlapped = 0;

                //Simulate PrepTime in Units
                List<DateTime> NewPrepTimes = new List<DateTime>();
                for (int i = 0; i < rentalBindingModel.PreparationTimeInDays; i++)
                    NewPrepTimes.Add(item.CurrentDate.AddDays(i));

                var bookedOverlapp = validatePuts
                               .Where(t => t.IsBookDay && NewPrepTimes.Contains(t.CurrentDate) && t.Unit == item.Unit);

                if (!bookedOverlapp.Any())
                    throw new ApplicationException("Cannot Update, Overlapped Preparation Times");
            }
            return true;
        }

        public static ValidatePutRentalViewModel ToViewModel(int unit, DateTime date, string type)
        {
            return new ValidatePutRentalViewModel()
            {
                CurrentDate = date,
                Unit = unit,
                IsCleanDay = type == "CleanDay" ? true : false,
                IsBookDay = type == "BookDay" ? true : false,

            };
        }
    }
}
