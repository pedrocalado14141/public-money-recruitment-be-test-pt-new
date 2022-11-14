using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VacationRental.Api.Domain.Contracts;
using VacationRental.Api.Domain.Models;

namespace VacationRental.Api.Domain.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly IDictionary<int, RentalViewModel> _rentals;
        
        public RentalRepository(IDictionary<int, RentalViewModel> rentals)
        {
            _rentals = rentals;
        }

        public async Task<IDictionary<int, RentalViewModel>> GetAllAsync()
            => await Task.FromResult(_rentals);

        public async Task<RentalViewModel> GetByIdAsync(int id)
        {
            return await Task.FromResult(_rentals[id]);
        }

        public async Task<ResourceIdViewModel> AddAsync(RentalViewModel entityViewModel)
        {
            var key = new ResourceIdViewModel { Id = _rentals.Keys.Count + 1 };

            var model = new RentalViewModel
            {
                Id = key.Id,
                Units = entityViewModel.Units,
                PreparationTimeInDays = entityViewModel.PreparationTimeInDays
            };
            _rentals.Add(key.Id, model);

            return await Task.FromResult(new ResourceIdViewModel()
            {
                Id = key.Id
            });
        }

        public async Task<ResourceIdViewModel> UpdateAsync(RentalViewModel entityViewModel)
        {
            _rentals[entityViewModel.Id] = entityViewModel;
            return await Task.FromResult(new ResourceIdViewModel()
            {
                Id = _rentals[entityViewModel.Id].Id
            });
        }

        public Task<ResourceIdViewModel> DeleteAsync(int rentalId)
        {
            throw new NotImplementedException();
        }
    }
}
