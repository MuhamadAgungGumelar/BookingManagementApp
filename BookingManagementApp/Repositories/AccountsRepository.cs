using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountsRepository : IAccountsRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountsRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Accounts> GetAll()
        {
            return _context.Set<Accounts>().ToList();
        }

        public Accounts? GetByGuid(Guid guid)
        {
            return _context.Set<Accounts>().Find(guid);
        }

        public Accounts? Create(Accounts account)
        {
            try
            {
                _context.Set<Accounts>().Add(account);
                _context.SaveChanges();
                return account;
            }
            catch
            {
                return new Accounts();
            }
        }

        public bool Update(Accounts account)
        {
            try
            {
                _context.Set<Accounts>().Update(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Guid guid)
        {
            try
            {
                var account = _context.Set<Accounts>().Find(guid);
                _context.Set<Accounts>().Remove(account);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
