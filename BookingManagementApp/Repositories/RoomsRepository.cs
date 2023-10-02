using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class RoomsRepository : IRoomsRepository
    {
        private readonly BookingManagementDbContext _context;
        public RoomsRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Rooms> GetAll()
        {
            return _context.Set<Rooms>().ToList();
        }

        public Rooms? GetByGuid(Guid guid)
        {
            return _context.Set<Rooms>().Find(guid);
        }

        public Rooms? Create(Rooms room)
        {
            try
            {
                _context.Set<Rooms>().Add(room);
                _context.SaveChanges();
                return room;
            }
            catch
            {
                return new Rooms();
            }
        }

        public bool Update(Rooms room)
        {
            try
            {
                _context.Set<Rooms>().Update(room);
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
                var room = _context.Set<Rooms>().Find(guid);
                _context.Set<Rooms>().Remove(room);
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
