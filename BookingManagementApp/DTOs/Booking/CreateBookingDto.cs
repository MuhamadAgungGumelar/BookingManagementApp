using BookingManagementApp.DTOs.Education;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.DTOs.Booking
{
    public class CreateBookingDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public StatuslLevel Status { get; set; }
        public string Remarks { get; set; }
        public Guid RoomGuid { get; set; }
        public Guid EmployeeGuid { get; set; }

        public static implicit operator Bookings(CreateBookingDto createBookingDto)
        {
            return new Bookings
            {
                StartDate = createBookingDto.StartDate,
                EndDate = createBookingDto.EndDate,
                Status = createBookingDto.Status,
                Remarks = createBookingDto.Remarks, 
                RoomGuid = createBookingDto.RoomGuid,
                EmployeeGuid = createBookingDto.EmployeeGuid,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
