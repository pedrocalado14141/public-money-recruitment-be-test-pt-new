
using System.Threading.Tasks;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Application.Interfaces
{
    public interface IRentalService
    {
        Task<RentalViewModel> GetById(int rentalId);
        Task<ResourceIdViewModel> Insert(RentalBindingModel rental);
        Task<ResourceIdViewModel> Update(int rentalId, RentalBindingModel rental);
    }
}
