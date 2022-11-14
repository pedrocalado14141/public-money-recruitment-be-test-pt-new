using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Domain.Contracts
{
    public interface IBookingRepository : IAsyncRepository<BookingViewModel> 
    {
        Task<IEnumerable<BookingViewModel>> GetAllByRentalIdAsync(int rentalId);
    }
}
