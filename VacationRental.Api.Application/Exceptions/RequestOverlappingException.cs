using System;
using System.Collections.Generic;
using System.Text;

namespace VacationRental.Api.Application.Exceptions
{
    public class RequestOverlappingException : Exception
    {
        public int Id { get; set; }
        public RequestOverlappingException() { }
        public RequestOverlappingException(string message) : base(message) { }
        public RequestOverlappingException(string message, int id) : this(message)
        {
            Id = id;
        }
    }
}
