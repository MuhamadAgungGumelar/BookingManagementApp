using BookingManagementApp.Utilities.Enums;

namespace BookingManagementApp.DTOs.Room
{
    public class RoomDetailDto
    {
        public Guid BookingGuid { get; set; }
        public string RoomName { get; set; }
        public StatuslLevel Status { get; set; }
        public int Floor { get; set; }
        public string BookedBy { get; set; }
    }
}
