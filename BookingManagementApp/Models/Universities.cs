using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name:"tb_m_universities")] // Membuat Tabel Universities
    public class Universities : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name:"code", TypeName = "nvarchar(50)")] // Membuat Column Tabel Code
        public string Code { get; set; }
        [Column(name: "name", TypeName = "nvarchar(100)")] // Membuat Column Tabel Name
        public string Name { get; set; }

        //Cardinality
        public ICollection<Educations>? Educations { get; set; }
    }
}
