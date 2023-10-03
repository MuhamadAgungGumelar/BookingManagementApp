using BookingManagementApp.Contracts;
using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.AccountRole;
using BookingManagementApp.Models;
using BookingManagementApp.Repositories;
using BookingManagementApp.Utilities.Handlers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

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
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var data = result.Select(x => (AccountRoleDto)x);

            return Ok(new ResponseOKHandler<IEnumerable<AccountRoleDto>>(data));
        }

        [HttpGet("{guid}")]
        public IActionResult Get(Guid guid)
        {
            var result = _accountRoleRepository.GetByGuid(guid);

            if (result is null)
            {
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)result));
        }

        [HttpPost]
        public IActionResult Create(CreateAccountRoleDto createAccountRoleDto)
        {
            try
            {
                var result = _accountRoleRepository.Create(createAccountRoleDto);

                return Ok(new ResponseOKHandler<AccountRoleDto>((AccountRoleDto)result));
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
        public IActionResult Update(AccountRoleDto accountRoleDto)
        {
            try
            {
                var entity = _accountRoleRepository.GetByGuid(accountRoleDto.Guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                AccountRoles toUpdate = accountRoleDto;
                toUpdate.CreatedDate = entity.CreatedDate;

                var result = _accountRoleRepository.Update(toUpdate);

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
                var entity = _accountRoleRepository.GetByGuid(guid);
                if (entity is null)
                {
                    return NotFound(new ResponseErrorHandler
                    {
                        Code = StatusCodes.Status404NotFound,
                        Status = HttpStatusCode.NotFound.ToString(),
                        Message = "Data Not Found"
                    });
                }

                var result = _accountRoleRepository.Delete(entity);


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
