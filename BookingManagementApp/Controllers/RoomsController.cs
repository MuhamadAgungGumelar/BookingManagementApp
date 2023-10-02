using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Room;
using BookingManagementApp.DTOs.University;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomsRepository _roomRepository;

        public RoomsController(IRoomsRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roomRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (RoomDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _roomRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((RoomDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateRoomDto createRoomDto)
        {
            var result = _roomRepository.Create(createRoomDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((RoomDto) result);
        }

        [HttpPut]
        public IActionResult Update(RoomDto roomDto)
        {
            var entity = _roomRepository.GetByGuid(roomDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Rooms toUpdate = roomDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _roomRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _roomRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _roomRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
