using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class UniversitiesRepository : IUniversitiesRepository
    {
        private readonly BookingManagementDbContext _context;
        public UniversitiesRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Universities> GetAll()
        {
            return _context.Set<Universities>().ToList();
        }

        public Universities? GetByGuid(Guid guid)
        {
            return _context.Set<Universities>().Find(guid);
        }

        public Universities? Create(Universities universities)
        {
            try
            {
                _context.Set<Universities>().Add(universities);
                _context.SaveChanges();
                return universities;
            }catch
            {
                return new Universities();
            }
        }

        public bool Update(Universities universities)
        {
            try
            {
                _context.Set<Universities>().Update(universities);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool Delete(Universities universities)
        {
            try
            {
                _context.Set<Universities>().Remove(universities);
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
