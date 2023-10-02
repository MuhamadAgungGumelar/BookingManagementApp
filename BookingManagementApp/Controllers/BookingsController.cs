using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingsRepository _bookingRepository;

        public BookingsController(IBookingsRepository bookingRepository)
        {
            _bookingRepository = bookingRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _bookingRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Bookings booking) 
        {
            var result = _bookingRepository.Create(booking);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Bookings booking)
        {
            var result = _bookingRepository.Update(booking);
            if (result is false)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var booking = _bookingRepository.GetByGuid(guid);

            if (booking is null)
            {
                return NotFound("Data Not Found");
            }

            _bookingRepository.Delete(guid);

            return Ok("Data deleted successfully");
        }
    }
}
