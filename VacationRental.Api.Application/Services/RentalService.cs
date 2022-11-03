using System.Threading.Tasks;
using VacationRental.Api.Application.Exceptions;
using VacationRental.Api.Application.Validations;
using VacationRental.Api.Application.Interfaces;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;
using System;
using System.Linq;

namespace VacationRental.Api.Application.Services
{
    public class RentalService : IRentalService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICalendarService _calendarService;

        public RentalService(IUnitOfWork unitOfWork, ICalendarService calendarService)
        {
            _unitOfWork = unitOfWork;
            _calendarService = calendarService;
        }

        public async Task<RentalViewModel> GetById(int rentalId)
        {
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(rentalId);

            if (rental == null) throw new NotFoundException("Rental not found", rentalId);
            return rental;
        }

        public async Task<ResourceIdViewModel> Insert(RentalBindingModel model)
        {
            if (model.PreparationTimeInDays <= 0) throw new NegativeNumberException("Must be positive", nameof(model.PreparationTimeInDays));

            return await _unitOfWork.RentalRepository.AddAsync(new RentalViewModel
            {
                Units = model.Units,
                PreparationTimeInDays = model.PreparationTimeInDays
            });
        }

        public async Task<ResourceIdViewModel> Update(int rentalId, RentalBindingModel rentalModel)
        {
            var bookings = await _unitOfWork.BookingRepository.GetAllByRentalIdAsync(rentalId);
            var rental = await _unitOfWork.RentalRepository.GetByIdAsync(rentalId);

            if (rental == null) throw new NotFoundException("Rental not found", rentalId);

            var lastBooking = bookings.OrderByDescending(x => x.LastNight).FirstOrDefault();

            if (lastBooking != null)
            {
                int nightsFromNow = (lastBooking.LastNight - DateTime.UtcNow).Days;
                var calendar = await _calendarService.GetAllAsync(rentalId, DateTime.UtcNow, nightsFromNow + rental.PreparationTimeInDays);

                // Check current booking has overlap
                var hasOverlapping = ValidationHelper.ValidateBooking(calendar, rentalModel, rental.Units);

                if (!hasOverlapping) throw new RequestOverlappingException("Rent overlapped", rentalId);
            }

            rental.Units = rentalModel.Units;
            rental.PreparationTimeInDays = rentalModel.PreparationTimeInDays;

            var updated = await _unitOfWork.RentalRepository.UpdateAsync(rental);
            if (updated.Id == 0)
                throw new NotFoundException("Update Failed", rentalId);

            return updated;
        }
    }
}
