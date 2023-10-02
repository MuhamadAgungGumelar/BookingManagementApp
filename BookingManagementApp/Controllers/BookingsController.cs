using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.DTOs.Education;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var data = result.Select(x => (BookingDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _bookingRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((BookingDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateBookingDto createBookingDto) 
        {
            var result = _bookingRepository.Create(createBookingDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((BookingDto)result);
        }

        [HttpPut]
        public IActionResult Update(BookingDto bookingDto)
        {
            var entity = _bookingRepository.GetByGuid(bookingDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Bookings toUpdate = bookingDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _bookingRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _bookingRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _bookingRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
