using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.DTOs.Role;
using BookingManagementApp.DTOs.Room;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeesRepository _employeeRepository;

        public EmployeesController(IEmployeesRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _employeeRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (EmployeeDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _employeeRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((EmployeeDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateEmployeeDto createEmployeeDto)
        {
            var result = _employeeRepository.Create(createEmployeeDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((EmployeeDto) result);
        }

        [HttpPut]
        public IActionResult Update(EmployeeDto employeeDto)
        {
            var entity = _employeeRepository.GetByGuid(employeeDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Employees toUpdate = employeeDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _employeeRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _employeeRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _employeeRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
