using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_rooms")]
    public class Rooms : BaseEntity
    {
        [Column(name: "name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
        [Column(name: "floor")]
        public int Floor { get; set; }
        [Column(name: "capacity")]
        public int Capacity { get; set; }
    }
}
