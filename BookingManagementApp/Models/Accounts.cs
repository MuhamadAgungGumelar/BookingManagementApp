using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_accounts")] // Membuat Tabel Accounts
    public class Accounts : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name: "password", TypeName = "nvarchar(max)")] // Membuat Column Tabel Password
        public string Password { get; set; } 
        [Column(name: "otp")] // Membuat Column Tabel Otp
        public int OTP { get; set; }
        [Column(name: "is_used")] // Membuat Column Tabel Is_Used
        public bool IsUsed { get; set; }
        [Column(name: "expired_time")] // Membuat Column Tabel Expired_Time
        public DateTime ExpiredTime { get; set; }
    }
}
