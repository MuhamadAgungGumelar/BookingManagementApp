using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Education;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.DTOs.Role;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (EducationDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<EducationDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _educationRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseOKHandler<EducationDto>((EducationDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateEducationDto createEducationDto)
        {
            try
            {
                var result = _educationRepository.Create(createEducationDto);

                return Ok(new ResponseOKHandler<EducationDto>((EducationDto) result));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpPut]
        public IActionResult Update(EducationDto educationDto)
        {
            try
            {
                var entity = _educationRepository.GetByGuid(educationDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                Educations toUpdate = educationDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _educationRepository.Update(toUpdate);

                return Ok(new ResponseOKHandler<string>("Data Updated"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            try
            {
                var entity = _educationRepository.GetByGuid(guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                var result = _educationRepository.Delete(entity);


                return Ok(new ResponseOKHandler<string>("Data Deleted"));
            }
            catch (ExceptionHandler ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ResponseErrorHandler
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Status = HttpStatusCode.InternalServerError.ToString(),
                    Message = "Failed to create data",
                    Error = ex.Message
                });
            }
        }
    }
}
