using BookingManagementApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_bookings")] // Membuat Tabel Bookings
    public class Bookings : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name: "start_date")] // Membuat Column Tabel Start_Date
        public DateTime StartDate { get; set; }
        [Column(name: "end_date")] // // Membuat Column Tabel End_Date
        public DateTime EndDate { get; set; }
        [Column(name: "status")] // Membuat Column Tabel Status
        public StatuslLevel Status {  get; set; }
        [Column(name: "remarks", TypeName = "nvarchar(max)")] // Membuat Column Tabel Remarks
        public string Remarks { get; set; }
        [Column(name: "room_id")] // Membuat Column Tabel Room_Id
        public Guid RoomGuid { get; set; }
        [Column(name: "employee_id")] // Membuat Column Tabel Employee_Id
        public Guid EmployeeGuid { get; set; }
    }
}
