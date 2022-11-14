using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Application.Models;
using VacationRental.Api.Application.Interfaces;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/bookings")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;
        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        [HttpGet("{bookingId:int}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([Required] int bookingId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingService.GetById(bookingId);
            return Ok(booking);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([Required] BookingBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var booking = await _bookingService.Insert(model);
            return Ok(booking);
        }
    }
}
