
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using VacationRental.Api.Application.Interfaces;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Application.ViewModels;
using VacationRental.Api.Domain.Contracts;

namespace VacationRental.Api.Application.Services
{
    public class CalendarService : ICalendarService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CalendarService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CalendarViewModel> GetAllAsync(int rentalId, DateTime start, int nights)
        {
            var rentals = await _unitOfWork.RentalRepository.GetAllAsync();
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();

            if (nights < 0)
                throw new ApplicationException("Nights must be positive");
            if (!rentals.ContainsKey(rentalId))
                throw new ApplicationException("Rental not found");

            var result = new CalendarViewModel
            {
                RentalId = rentalId,
                Dates = new List<CalendarDateViewModel>()
            };
            int preparationTime = rentals[rentalId].PreparationTimeInDays;
            int totalUnits = rentals[rentalId].Units;

            //Add Units to Clean
            List<UnitsToCleanViewModel> masterUnitClean = new List<UnitsToCleanViewModel>();

            //Initial Units Map to Read
            List<AvailableUnitsViewModel> unitsToCleanList = new List<AvailableUnitsViewModel>();
            for (int i = 0; i < totalUnits; i++)
            {
                AvailableUnitsViewModel unitsToClean = new AvailableUnitsViewModel();
                unitsToClean.lastDayBooked = DateTime.Now.Date;
                unitsToClean.Unit = i + 1;
                unitsToClean.Isblocked = false;
                unitsToClean.PrepTimeDates = new List<DateTime>();
                unitsToCleanList.Add(unitsToClean);
            }

            for (var i = 0; i < nights + preparationTime; i++)
            {
                var date = new CalendarDateViewModel
                {
                    Date = start.Date.AddDays(i),
                    Bookings = new List<CalendarBookingViewModel>(),
                    PreparationTimes = new List<UnitViewModel>()
                };

                //Reset Blocked units

                var resetBlock = unitsToCleanList
                    .Where(r => 
                    r.Isblocked && 
                    !r.PrepTimeDates.Any(an=> an.Equals(date.Date)) &&
                    (date.Date.Date < r.lastDayBooked.Date || // if current date < last day to get out
                    date.Date.Date > r.lastDayBooked.Date)).ToList(); // if current date > last day to get out

                resetBlock.ForEach(r => r.Isblocked = false);

                foreach (var booking in bookings.Values)
                {
                    if (booking.RentalId == rentalId)
                    {
                        if (i < nights &&booking.Start <= date.Date && booking.Start.AddDays(booking.Nights) > date.Date)
                        {
                            var unitAvailable = unitsToCleanList.Where(r => !r.Isblocked).FirstOrDefault();

                            date.Bookings.Add(new CalendarBookingViewModel { Id = booking.Id, Unit = unitAvailable.Unit });

                            //Update info to Initial Units Map (unitsToCleanList)
                            unitAvailable.lastDayBooked = booking.LastNight;
                            unitAvailable.Isblocked = true;

                            List<DateTime> addPrepTimes = new List<DateTime>();
                            for (int pr = 0; pr < preparationTime; pr++)
                                addPrepTimes.Add(booking.Start.AddDays(booking.Nights + pr));

                            unitAvailable.PrepTimeDates = addPrepTimes;

                            if (!masterUnitClean.Where(r => r.BookingId == booking.Id).Any())
                            {
                                for (int prep = 0; prep < preparationTime; prep++)
                                {
                                    var prepDate = booking.Start.AddDays(booking.Nights + prep);
                                    UnitsToCleanViewModel newUnit = new UnitsToCleanViewModel()
                                    {
                                        Date = prepDate,
                                        BookingId = booking.Id,
                                        Isblocked = true,
                                        Unit = unitAvailable.Unit
                                    };
                                    masterUnitClean.Add(newUnit);
                                }
                            }
                        }
                    }
                }

                //Add Preparation Times
                var unitCleanAux = masterUnitClean.Where(r => r.Date == date.Date);

                if (unitCleanAux.Any())
                {
                    date.PreparationTimes = new List<UnitViewModel>();

                    foreach (var item in unitCleanAux)
                    {
                        date.PreparationTimes.Add(new UnitViewModel()
                        {
                            Unit = item.Unit
                        });
                    }
                }

                result.Dates.Add(date);
            }
            return result;
        }
    }
}
