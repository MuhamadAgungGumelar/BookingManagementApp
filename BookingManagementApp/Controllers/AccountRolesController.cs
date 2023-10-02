using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.AccountRole;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountRolesController : ControllerBase
    {
        private readonly IAccountRolesRepository _accountRoleRepository;

        public AccountRolesController(IAccountRolesRepository accountRoleRepository)
        {
            _accountRoleRepository = accountRoleRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRoleRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            var data = result.Select(x => (AccountRoleDto)x);

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((AccountRoleDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            var result = _accountRoleRepository.Create(createAccountRoleDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((AccountRoleDto)result);
        }

        [HttpPut]
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            AccountRoles toUpdate = accountRoleDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _accountRoleRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _accountRoleRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _accountRoleRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
