using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_educations")]
    public class Educations : BaseEntity
    {
        [Column(name: "major", TypeName = "nvarchar(100)")]
        public string Major { get; set; }
        [Column(name: "degree", TypeName = "nvarchar(100)")]
        public string Degree { get; set; }
        [Column(name: "gpa")]
        public float Gpa { get; set; }
        [Column(name: "university_guid")]
        public Guid UniversityGuid { get; set; }
    }
}
