using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Domain.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        private readonly IDictionary<int, BookingViewModel> _bookings;

        private IBookingRepository _bookingRepository;
        private IRentalRepository _rentalRepository;

        public UnitOfWork(IDictionary<int, RentalViewModel> rentals, IDictionary<int, BookingViewModel> bookings)
        {
            _rentals = rentals;
            _bookings = bookings;
        }

        public IBookingRepository BookingRepository =>
            _bookingRepository = new BookingRepository(_bookings);

        public IRentalRepository RentalRepository =>
            _rentalRepository = new RentalRepository(_rentals);

    }
}
