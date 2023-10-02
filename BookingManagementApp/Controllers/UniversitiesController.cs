using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;
using BookingManagementApp.DTOs.University;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UniversitiesController : ControllerBase
    {
        private readonly IUniversitiesRepository _universitiesRepository;

        public UniversitiesController(IUniversitiesRepository universitiesRepository)
        {
            _universitiesRepository = universitiesRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _universitiesRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (UniversityDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _universitiesRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((UniversityDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateUniversityDto universityDto)
        {
            var result = _universitiesRepository.Create(universityDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((UniversityDto) result);
        }

        [HttpPut]
        public IActionResult Update(UniversityDto universityDto)
        {
            var entity = _universitiesRepository.GetByGuid(universityDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Universities toUpdate = universityDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _universitiesRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _universitiesRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _universitiesRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
