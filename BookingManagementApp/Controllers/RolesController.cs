using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Role;
using BookingManagementApp.DTOs.University;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RolesController : ControllerBase
    {
        private readonly IRolesRepository _roleRepository;

        public RolesController(IRolesRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _roleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (RoleDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _roleRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((RoleDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateRoleDto createRoleDto)
        {
            var result = _roleRepository.Create(createRoleDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((RoleDto) result);
        }

        [HttpPut]
        public IActionResult Update(RoleDto roleDto)
        {
            var entity = _roleRepository.GetByGuid(roleDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Roles toUpdate = roleDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _roleRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _roleRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _roleRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
