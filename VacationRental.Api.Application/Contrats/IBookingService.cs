using System.Threading.Tasks;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Application.Interfaces
{
    public interface IBookingService
    {
        Task<BookingViewModel> GetById(int bookingId);
        Task<ResourceIdViewModel> Insert(BookingBindingModel booking);
    }
}
