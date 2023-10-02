using BookingManagementApp.DTOs.Account;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.AccountRole
{
    public class AccountRoleDto
    {
        public Guid Guid { get; set; }
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }

        public static explicit operator AccountRoleDto(AccountRoles accountRoles)
        {
            return new AccountRoleDto
            {
                Guid = accountRoles.Guid,
                AccountGuid = accountRoles.AccountGuid,
                RoleGuid = accountRoles.RoleGuid
            };
        }

        public static implicit operator AccountRoles(AccountRoleDto accountRoleDto)
        {
            return new AccountRoles
            {
                Guid = accountRoleDto.Guid,
                AccountGuid = accountRoleDto.AccountGuid,
                RoleGuid = accountRoleDto.RoleGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
