using BookingManagementApp.Contracts;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("/api[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountsRepository _accountRepository;

        public AccountsController(IAccountsRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _accountRepository.GetAll();
            if (!result.Any())
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Create(Accounts account)
        {
            var result = _accountRepository.Create(account);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Update(Accounts account)
        {
            var result = _accountRepository.Update(account);
            if (result is false)
            {
                return BadRequest("Failed to update data");
            }

            return Ok(result);
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var account = _accountRepository.GetByGuid(guid);

            if (account is null)
            {
                return NotFound("Data Not Found");
            }

            _accountRepository.Delete(guid);

            return Ok("Data deleted successfully");
        }
    }
}
