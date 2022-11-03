using System;

namespace VacationRental.Api.Application.Exceptions
{
    public class NegativeNumberException : Exception
    {
        public string Number { get; set; }
        public NegativeNumberException() { }
        public NegativeNumberException(string message) : base(message) { }
        public NegativeNumberException(string message, string number) : this(message)
        {
            Number = number;
        }
    }
}
