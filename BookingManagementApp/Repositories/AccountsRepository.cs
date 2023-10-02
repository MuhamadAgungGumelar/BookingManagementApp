using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountsRepository : GeneralRepository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(BookingManagementDbContext context):base (context) { }
    }
}
