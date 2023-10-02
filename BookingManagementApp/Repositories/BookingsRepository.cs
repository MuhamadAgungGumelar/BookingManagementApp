using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class BookingsRepository : IBookingsRepository
    {
        private readonly BookingManagementDbContext _context;
        public BookingsRepository(BookingManagementDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Bookings> GetAll()
        {
            return _context.Set<Bookings>().ToList();
        }

        public Bookings? GetByGuid(Guid guid)
        {
            return _context.Set<Bookings>().Find(guid);
        }

        public Bookings? Create(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Add(booking);
                _context.SaveChanges();
                return booking;
            }
            catch
            {
                return new Bookings();
            }
        }

        public bool Update(Bookings booking)
        {
            try
            {
                _context.Set<Bookings>().Update(booking);
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
                var booking = _context.Set<Bookings>().Find(guid);
                _context.Set<Bookings>().Remove(booking);
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
