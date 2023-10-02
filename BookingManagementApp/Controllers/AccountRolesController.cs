using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
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

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(AccountRoles accountRole)
        {
            var result = _accountRoleRepository.Create(accountRole);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(AccountRoles accountRole)
        {
            var result = _accountRoleRepository.Update(accountRole);
            if (result is false)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var accountRole = _accountRoleRepository.GetByGuid(guid);

            if (accountRole is null)
            {
                return NotFound("Data Not Found");
            }

            _accountRoleRepository.Delete(guid);

            return Ok("Data deleted successfully");
        }
    }
}
