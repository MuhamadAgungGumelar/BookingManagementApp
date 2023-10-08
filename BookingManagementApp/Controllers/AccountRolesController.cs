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
        private readonly IEmployeesRepository _employeesRepository;
        private readonly IRolesRepository _rolesRepository;
        private readonly IAccountsRepository _accountsRepository;

        public AccountRolesController(IAccountRolesRepository accountRoleRepository, IEmployeesRepository employeesRepository, IRolesRepository rolesRepository, IAccountsRepository accountsRepository)
        {
            _accountRoleRepository = accountRoleRepository;
            _employeesRepository = employeesRepository;
            _rolesRepository = rolesRepository;
            _accountsRepository = accountsRepository;
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

        [HttpGet("DetailAccountRole")]
        public IActionResult DetailAccountRole()
        {
            var accountRoles = _accountRoleRepository.GetAll();
            var employees = _employeesRepository.GetAll();
            var roles = _rolesRepository.GetAll();
            var accounts = _accountsRepository.GetAll();

            if (!(employees.Any() && roles.Any() && accountRoles.Any()))
            {
                //Apabila data gagal ditemukan, akan menampilkan pesan data tidak ditemukan
                return NotFound(new ResponseErrorHandler
                {
                    Code = StatusCodes.Status404NotFound,
                    Status = HttpStatusCode.NotFound.ToString(),
                    Message = "Data Not Found"
                });
            }

            var accountRoleDetails = from emp in employees
                                  join acc in accounts on emp.Guid equals acc.Guid
                                  join acr in accountRoles on acc.Guid equals acr.AccountGuid
                                  join rl in roles on acr.RoleGuid equals rl.Guid
                                  select new DetailAccountRoleDto
                                  {
                                      Guid = acr.Guid,
                                      AccountGuid = acc.Guid,
                                      RoleGuid = rl.Guid,
                                      FullName = string.Concat(emp.FirstName, " ", emp.LastName),
                                      RoleName = rl.Name
                                  };

            return Ok(new ResponseOKHandler<IEnumerable<DetailAccountRoleDto>>(accountRoleDetails));
        }
    }
}
