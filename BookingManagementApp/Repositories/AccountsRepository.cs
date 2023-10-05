using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountsRepository : GeneralRepository<Accounts>, IAccountsRepository
    {
        public AccountsRepository(BookingManagementDbContext context):base (context) { }

        public Accounts? GetByEmail(string email)
        {
            return _context.Set<Accounts>().FirstOrDefault(x => x.Employees != null && x.Employees.Email == email);
        }

        public Universities? GetByUniversityName(string name)
        {
            return _context.Set<Universities>().FirstOrDefault(x => x != null && x.Name != null);
        }
    }
}
