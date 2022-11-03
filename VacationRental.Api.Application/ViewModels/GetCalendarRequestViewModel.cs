using System.ComponentModel.DataAnnotations;
using System;

namespace VacationRental.Api.Application.ViewModels
{
    public class GetCalendarRequestViewModel
    {
        [Required]
        public int RentalId { get; set; }
        [Required]
        public DateTime Start { get; set; }
        [Required]
        public int Nights { get; set; }
    }
}
