using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;
using System;

namespace VacationRental.Api.Domain.Repositories
{
    public class BookingRepository : IBookingRepository
    {
        private readonly IDictionary<int, BookingViewModel> _bookings;

        public BookingRepository(IDictionary<int, BookingViewModel> bookings)
        {
            _bookings = bookings;
        }

        public async Task<IDictionary<int, BookingViewModel>> GetAllAsync()
            => await Task.FromResult(_bookings);

        public async Task<BookingViewModel> GetByIdAsync(int id)
        {
            return await Task.FromResult(_bookings[id]);
        }

        public async Task<ResourceIdViewModel> AddAsync(BookingViewModel model)
        {
            var key = new ResourceIdViewModel { Id = _bookings.Keys.Count + 1 };

            _bookings.Add(key.Id, new BookingViewModel
            {
                Id = key.Id,
                Nights = model.Nights,
                RentalId = model.RentalId,
                Start = model.Start.Date,
                LastNight = model.LastNight
            });

            return await Task.FromResult(key);
        }

        public async Task<IEnumerable<BookingViewModel>> GetAllByRentalIdAsync(int rentalId)
            => await Task.FromResult(_bookings.Values.Where(e => e.RentalId == rentalId));

        public Task<ResourceIdViewModel> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ResourceIdViewModel> UpdateAsync(BookingViewModel entityViewModel)
        {
            throw new NotImplementedException();
        }
    }
}
