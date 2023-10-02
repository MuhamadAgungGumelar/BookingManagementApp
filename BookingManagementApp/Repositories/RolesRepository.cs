using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class RolesRepository : IRolesRepository
    {
        private readonly BookingManagementDbContext _context;
        public RolesRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Roles> GetAll()
        {
            return _context.Set<Roles>().ToList();
        }

        public Roles? GetByGuid(Guid guid)
        {
            return _context.Set<Roles>().Find(guid);
        }

        public Roles? Create(Roles role)
        {
            try
            {
                _context.Set<Roles>().Add(role);
                _context.SaveChanges();
                return role;
            }
            catch
            {
                return new Roles();
            }
        }

        public bool Update(Roles role)
        {
            try
            {
                _context.Set<Roles>().Update(role);
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
                var role = _context.Set<Roles>().Find(guid);
                _context.Set<Roles>().Remove(role);
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
