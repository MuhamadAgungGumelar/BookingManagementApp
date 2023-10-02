using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class EducationsRepository : IEducationsRepository
    {
        private readonly BookingManagementDbContext _context;
        public EducationsRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Educations> GetAll()
        {
            return _context.Set<Educations>().ToList();
        }

        public Educations? GetByGuid(Guid guid)
        {
            return _context.Set<Educations>().Find(guid);
        }

        public Educations? Create(Educations education)
        {
            try
            {
                _context.Set<Educations>().Add(education);
                _context.SaveChanges();
                return education;
            }
            catch
            {
                return new Educations();
            }
        }

        public bool Update(Educations education)
        {
            try
            {
                _context.Set<Educations>().Update(education);
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
                var education = _context.Set<Educations>().Find(guid);
                _context.Set<Educations>().Remove(education);
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
