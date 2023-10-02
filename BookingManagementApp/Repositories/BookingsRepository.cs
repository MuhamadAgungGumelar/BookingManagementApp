using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class BookingsRepository : GeneralRepository<Bookings>, IBookingsRepository
    {
        public BookingsRepository(BookingManagementDbContext context):base (context) { }
    }
}
