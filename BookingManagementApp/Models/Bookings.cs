using BookingManagementApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_bookings")]
    public class Bookings : BaseEntity
    {
        [Column(name: "start_date")]
        public DateTime StartDate { get; set; }
        [Column(name: "end_date")]
        public DateTime EndDate { get; set; }
        [Column(name: "status")]
        public StatuslLevel Status {  get; set; }
        [Column(name: "remarks", TypeName = "nvarchar(max)")]
        public string Remarks { get; set; }
        [Column(name: "room_id")]
        public Guid RoomGuid { get; set; }
        [Column(name: "employee_id")]
        public Guid EmployeeGuid { get; set; }
    }
}
