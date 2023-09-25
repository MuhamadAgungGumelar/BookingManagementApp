using BookingManagementApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_employees")] // Membuat Tabel Employees
    public class Employees : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    { 
        [Column(name: "nik", TypeName = "nchar(6)")] // Membuat Column Tabel Nik
        public string NIK { get; set; }
        [Column(name: "first_name", TypeName = "nvarchar(100)")] // Membuat Column Tabel First_Name
        public string FirstName { get; set; }
        [Column(name: "last_name", TypeName = "nvarchar(100)")] // Membuat Column Tabel Last_Name
        public string? LastName { get; set; }
        [Column(name: "birth_date")]  // Membuat Column Tabel Birth_Date
        public DateTime BirthDate { get; set; }
        [Column(name: "gender")] // Membuat Column Tabel Gender
        public GenderLevel Gender { get; set; }
        [Column(name: "hiring_date")] // Membuat Column Tabel Hiring_Date
        public DateTime HiringDate { get; set; }
        [Column(name: "email", TypeName = "nvarchar(100)")] // Membuat Column Tabel Email
        public string Email { get; set; }
        [Column(name: "phone_number", TypeName = "nvarchar(20)")] // Membuat Column Tabel Phone_Number
        public string PhoneNumber { get; set; }
    }
}
