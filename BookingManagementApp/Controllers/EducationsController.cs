using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
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

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Educations education)
        {
            var result = _educationRepository.Create(education);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Educations education)
        {
            var result = _educationRepository.Update(education);
            if (result is false)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var education = _educationRepository.GetByGuid(guid);

            if (education is null)
            {
                return NotFound("Data Not Found");
            }

            _educationRepository.Delete(guid);

            return Ok("Data deleted successfully");
        }
    }
}
