using BookingManagementApp.Utilities.Enums;

namespace BookingManagementApp.DTOs.Booking
{
    public class BookingDetailDto
    {
        public Guid Guid { get; set; }
        public string NIK { get; set; }
        public string BookedBy { get; set; }
        public string RoomName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatuslLevel Status { get; set; }
        public string Remarks { get; set; }
    }
}
