using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_roles")] // Membuat Tabel Roles
    public class Roles : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name: "name", TypeName = "nvarchar(100)")] // Membuat Column Tabel Name
        public string Name { get; set; }
    }
}
