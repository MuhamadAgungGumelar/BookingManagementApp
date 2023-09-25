using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_accounts_roles")]
    public class AccountRoles : BaseEntity
    {
        [Column(name: "account guid")]
        public Guid AccountGuid { get; set; }
        [Column(name: "role guid")]
        public Guid RoleGuid { get; set; }
    }
}
