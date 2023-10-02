using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class AccountRolesRepository : IAccountRolesRepository
    {
        private readonly BookingManagementDbContext _context;
        public AccountRolesRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<AccountRoles> GetAll()
        {
            return _context.Set<AccountRoles>().ToList();
        }

        public AccountRoles? GetByGuid(Guid guid)
        {
            return _context.Set<AccountRoles>().Find(guid);
        }

        public AccountRoles? Create(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Add(accountRole);
                _context.SaveChanges();
                return accountRole;
            }
            catch
            {
                return new AccountRoles();
            }
        }

        public bool Update(AccountRoles accountRole)
        {
            try
            {
                _context.Set<AccountRoles>().Update(accountRole);
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
                var accountRole = _context.Set<AccountRoles>().Find(guid);
                _context.Set<AccountRoles>().Remove(accountRole);
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
