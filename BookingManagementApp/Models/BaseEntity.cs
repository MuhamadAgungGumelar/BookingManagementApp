using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public abstract class BaseEntity // Membuat Class Base Entity Untuk Blueprint Atribut atau Kolom Guid, CreatedDate, ModifiedDate
    {
        [Key, Column(name: "guid")] // Membuat Column Tabel Guid
        public Guid Guid { get; set; }
        [Column(name: "created_date")] // Membuat Column Tabel Created_Date
        public DateTime CreatedDate { get; set; }
        [Column(name: "modified_date")] // Membuat Column Tabel Modified_Date
        public DateTime ModifiedDate { get; set; }
    }
}
