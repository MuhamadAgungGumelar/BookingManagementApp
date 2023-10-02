using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enums;

namespace BookingManagementApp.DTOs.Booking
{
    public class BookingDto
    {
        public Guid Guid { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatuslLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static explicit operator BookingDto(Bookings bookings)
        {
            return new BookingDto
            {
                Guid = bookings.Guid,
                StartDate = bookings.StartDate,
                EndDate = bookings.EndDate,
                Status = bookings.Status,
                Remarks = bookings.Remarks,
                RoomGuid = bookings.RoomGuid,
                EmployeeGuid = bookings.EmployeeGuid
            };
        }

        public static implicit operator Bookings(BookingDto bookingDto)
        {
            return new Bookings
            {
                Guid = bookingDto.Guid,
                StartDate = bookingDto.StartDate,
                EndDate = bookingDto.EndDate,
                Status = bookingDto.Status,
                Remarks = bookingDto.Remarks,
                RoomGuid = bookingDto.RoomGuid, 
                EmployeeGuid = bookingDto.EmployeeGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
