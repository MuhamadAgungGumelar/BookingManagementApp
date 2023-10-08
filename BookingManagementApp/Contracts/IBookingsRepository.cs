using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IBookingsRepository: IGeneralRepository<Bookings> 
    {
        IEnumerable<Bookings> GetBookingsForDate(DateTime date);
        IEnumerable<BookingLengthDto> GetBookingLengthOnWeekdays();
    }
}
