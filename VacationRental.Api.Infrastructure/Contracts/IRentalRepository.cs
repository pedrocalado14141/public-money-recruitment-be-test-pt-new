using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Domain.Contracts
{
    public interface IRentalRepository : IAsyncRepository<RentalViewModel>
    {
        
    }
}
