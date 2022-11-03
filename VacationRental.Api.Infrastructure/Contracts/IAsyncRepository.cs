using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Domain.Contracts
{
    public interface IAsyncRepository<TEntity> where TEntity : class
    {
        Task<IDictionary<int, TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(int id);
        Task<ResourceIdViewModel> DeleteAsync(int id);
        Task<ResourceIdViewModel> AddAsync(TEntity entityViewModel);
        Task<ResourceIdViewModel> UpdateAsync(TEntity entityViewModel);
    }
}
