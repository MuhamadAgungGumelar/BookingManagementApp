using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BookingManagementApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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

            var data = result.Select(x => (AccountDto)x);

            return Ok(data);
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _accountRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound("Data Not Found");
            }

            return Ok((AccountDto) result);
        }

        [HttpPost]
        public IActionResult Create(CreateAccountDto createAccountDto)
        {
            var result = _accountRepository.Create(createAccountDto);
            if (result is null)
            {
                return BadRequest("Failed to create data");
            }

            return Ok((AccountDto)result);
        }

        [HttpPut]
        public IActionResult Update(AccountDto accountDto)
        {
            var entity = _accountRepository.GetByGuid(accountDto.Guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            Accounts toUpdate = accountDto;
            toUpdate.CreatedDate = entity.CreatedDate;

            var result = _accountRepository.Update(toUpdate);
            if (!result)
            {
                return BadRequest("Failed to update data");
            }

            return Ok("Data Updated");
        }

        [HttpDelete("{guid}")]
        public IActionResult Delete(Guid guid)
        {
            var entity = _accountRepository.GetByGuid(guid);
            if (entity is null)
            {
                return NotFound("Id Not Found");
            }

            var result = _accountRepository.Delete(entity);
            if (!result)
            {
                return BadRequest("Failed to delete data");
            }

            return Ok("Data Deleted");
        }
    }
}
