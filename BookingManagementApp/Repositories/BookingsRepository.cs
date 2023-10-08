using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class BookingsRepository : GeneralRepository<Bookings>, IBookingsRepository
    {
        public BookingsRepository(BookingManagementDbContext context):base (context) { }

        public IEnumerable<Bookings> GetBookingsForDate(DateTime date)
        {
            // Mengambil daftar pemesanan pada tanggal tertentu
            // Tanggal mulai Booking kurang dari hari ini, dan tanggal akhir booking lebih dari hari ini
            return _context.Bookings.Where(booking => booking.StartDate.Date <= date.Date && booking.EndDate.Date >= date.Date).ToList();
        }

        public IEnumerable<BookingLengthDto> GetBookingLengthOnWeekdays()
        {
            // Membuat List Baru untuk menampilkan data ke user
            var bookingLengths = new List<BookingLengthDto>();

            // Mengambil semua data booking pada database
            var bookings = _context.Bookings.ToList();

            var rooms = _context.Rooms.ToList();

            foreach (var booking in bookings)
            {
                if (booking.Rooms != null)
                {
                    // Menghitung berapa lama peminjaman dalam hari
                    var bookingDuration = (booking.EndDate.Date - booking.StartDate.Date).Days;

                    // Mengabaikan hari Sabtu dan Minggu
                    var weekdaysCount = Enumerable.Range(0, bookingDuration + 1)
                        .Select(offset => booking.StartDate.AddDays(offset).DayOfWeek)
                        .Count(dayOfWeek => dayOfWeek != DayOfWeek.Saturday && dayOfWeek != DayOfWeek.Sunday);

                    // Menyajikan data ke user dengan atribut roomGuid, roomName dan bookinglenght
                    var bookingLength = new BookingLengthDto
                    {
                        RoomGuid = booking.Rooms.Guid,
                        RoomName = booking.Rooms.Name,
                        BookingLength = weekdaysCount
                    };

                    // Menyimpan data baru yang sudah dihitung jumlah hari booking ke list Baru
                    bookingLengths.Add(bookingLength);
                }
            }

            // Mengembalikan nilai list baru yang sudah memiliki jumlah hari booking yang dihitung
            return bookingLengths;
        }
    }
}
