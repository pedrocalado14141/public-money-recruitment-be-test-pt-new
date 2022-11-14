using System;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Application.Exceptions;
using VacationRental.Api.Application.Interfaces;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalendarService _calendarService;

        public BookingService(IUnitOfWork unitOfWork, ICalendarService calendarService)
        {
            _unitOfWork = unitOfWork;
            _calendarService = calendarService;
        }

        public async Task<BookingViewModel> GetById(int bookingId)
        {
            var booking = await _unitOfWork.BookingRepository.GetByIdAsync(bookingId);
            if (booking == null)
                throw new NotFoundException("Rental not found", bookingId);

            return booking;
        }

        public async Task<ResourceIdViewModel> Insert(BookingBindingModel model)
        {
            var rentals = await _unitOfWork.RentalRepository.GetAllAsync();
            var bookings = await _unitOfWork.BookingRepository.GetAllAsync();

            if (model.Nights <= 0)
                throw new ApplicationException("Nigts must be positive");
            if (model.Start.Date < DateTime.Now.Date)
                throw new ApplicationException("Stat Date cannot be old");
            if (!rentals.ContainsKey(model.RentalId))
                throw new ApplicationException("Rental not found");


            int prepTime = rentals[model.RentalId].PreparationTimeInDays;
            for (var i = 0; i < model.Nights; i++)
            {
                var count = 0;
                foreach (var booking in bookings.Values.Where(r=> r.RentalId == model.RentalId))
                {
                    int getBookingBeforePrepTimes = prepTime + 1;
                    if (booking.Start <= model.Start.Date && booking.Start.AddDays(booking.Nights + prepTime) > model.Start.Date
                        || (booking.Start < model.Start.AddDays(model.Nights) && booking.Start.AddDays(booking.Nights + prepTime) >= model.Start.AddDays(model.Nights))
                        || (booking.Start > model.Start && booking.Start.AddDays(booking.Nights + prepTime) < model.Start.AddDays(model.Nights)))
                    {
                        count++;
                    }
                }
                if (count >= rentals[model.RentalId].Units)
                    throw new ApplicationException("Not available");
            }

            return await _unitOfWork.BookingRepository.AddAsync(new BookingViewModel()
            {
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start.Date,
                LastNight = model.Start.AddDays(model.Nights)
            });
        }
    }
}
