using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountRolesRepository : GeneralRepository<AccountRoles>, IAccountRolesRepository
    {
        public AccountRolesRepository(BookingManagementDbContext context):base (context) { }
    }
}
