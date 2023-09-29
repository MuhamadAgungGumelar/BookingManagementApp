using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_rooms")] // Membuat Tabel Rooms
    public class Rooms : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name: "name", TypeName = "nvarchar(100)")] // Membuat Column Tabel Name
        public string Name { get; set; }
        [Column(name: "floor")] // Membuat Column Tabel Floor
        public int Floor { get; set; }
        [Column(name: "capacity")] // Membuat Column Tabel Capacity
        public int Capacity { get; set; }

        //Cardinality
        public  ICollection<Bookings> Bookings { get; set; }
    }
}
