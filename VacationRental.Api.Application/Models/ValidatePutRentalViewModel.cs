using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Api.Application.Models
{
    public class ValidatePutRentalViewModel
    {
        public int Unit { get; set; }
        public DateTime CurrentDate { get; set; }
        public bool IsCleanDay { get; set; }
        public bool IsBookDay { get; set; }
    }

    public class ValidateAuxPut
    {
        public bool MoveForward { get; set; }
        public int Unit { get; set; }
    }
}
