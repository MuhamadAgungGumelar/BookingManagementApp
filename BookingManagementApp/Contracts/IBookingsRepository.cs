using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IBookingsRepository
    {
        IEnumerable<Bookings> GetAll();
        Bookings? GetByGuid(Guid guid);
        Bookings? Create(Bookings booking);
        bool Update(Bookings booking);
        bool Delete(Guid guid);
    }
}
