using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VacationRental.Api.Application.Interfaces;
using VacationRental.Api.Application.Models;

namespace VacationRental.Api.Controllers
{
    [Route("api/v1/rentals")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly IRentalService _rentalService;
        public RentalsController(IRentalService rentalService)
        {
            _rentalService = rentalService;
        }

        [HttpGet]
        [Route("{rentalId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get([Required] int rentalId)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rental = await _rentalService.GetById(rentalId);
            return Ok(rental);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([Required] RentalBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rental = await _rentalService.Insert(model);
            return CreatedAtAction("Get", new { rentalId = rental.Id}, rental);
        }

        [HttpPut]
        [Route("{rentalId:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status304NotModified)]
        public async Task<IActionResult> Put([Required] int rentalId, [Required] RentalBindingModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var rental = await _rentalService.Update(rentalId, model);
            return Ok(rental);
        }
    }
}
