using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VacationRental.Api.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IBookingRepository BookingRepository { get; }
        IRentalRepository RentalRepository { get; }
    }
}
