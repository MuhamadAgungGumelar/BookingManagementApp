using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_educations")] // Membuat Tabel Educations
    public class Educations : BaseEntity // Inheritence Ke Class BaseEntity Abstracts
    {
        [Column(name: "major", TypeName = "nvarchar(100)")] // Membuat Column Tabel Major
        public string Major { get; set; }
        [Column(name: "degree", TypeName = "nvarchar(100)")] // Membuat Column Tabel Degree
        public string Degree { get; set; }
        [Column(name: "gpa")] // Membuat Column Tabel GPA
        public float Gpa { get; set; }
        [Column(name: "university_guid")] // Membuat Column Tabel University_Guid
        public Guid UniversityGuid { get; set; }


        public Universities? universities { get; set; }
        public Employees? Employees { get; set; }
    }
}
