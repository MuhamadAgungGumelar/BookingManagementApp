using BookingManagementApp.DTOs.Account;
using BookingManagementApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.DTOs.AccountRole
{
    public class CreateAccountRoleDto
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }


        public static implicit operator AccountRoles(CreateAccountRoleDto createAccountRoleDto)
        {
            return new AccountRoles
            {
                AccountGuid = createAccountRoleDto.AccountGuid,
                RoleGuid = createAccountRoleDto.RoleGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
