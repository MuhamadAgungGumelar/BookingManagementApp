using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class EmployeesRepostitory : IEmployeesRepository
    {
        private readonly BookingManagementDbContext _context;
        public EmployeesRepostitory(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employees> GetAll()
        {
            return _context.Set<Employees>().ToList();
        }

        public Employees? GetByGuid(Guid guid)
        {
            return _context.Set<Employees>().Find(guid);
        }

        public Employees? Create(Employees employee)
        {
            try
            {
                _context.Set<Employees>().Add(employee);
                _context.SaveChanges();
                return employee;
            }
            catch
            {
                return new Employees();
            }
        }

        public bool Update(Employees employee)
        {
            try
            {
                _context.Set<Employees>().Update(employee);
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
                var employee = _context.Set<Employees>().Find(guid);
                _context.Set<Employees>().Remove(employee);
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
