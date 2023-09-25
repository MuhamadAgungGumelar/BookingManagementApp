using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    public abstract class BaseEntity
    {
        [Key, Column(name: "guid")]
        public Guid Guid { get; set; }
        [Column(name: "created_date")]
        public DateTime CreatedDate { get; set; }
        [Column(name: "modified_date")]
        public DateTime ModifiedDate { get; set; }
    }
}
