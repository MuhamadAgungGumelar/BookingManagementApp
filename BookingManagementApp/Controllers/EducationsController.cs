using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Education;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EducationsController : ControllerBase
    {
        private readonly IEducationsRepository _educationRepository;

        public EducationsController(IEducationsRepository educationRepository)
        {
            _educationRepository = educationRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _educationRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (EducationDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((EducationDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            var result = _educationRepository.Create(createEducationDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EducationDto) result);
        }

        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {
            var entity = _educationRepository.GetByGuid(educationDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Educations toUpdate = educationDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _educationRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _educationRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _educationRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
