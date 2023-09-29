using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_accounts_roles")] // Membuat Tabel Account Roles
    public class AccountRoles : BaseEntity // Inheritence Ke Class BaseEntity Abstract
    {
        [Column(name: "account guid")] // Membuat Column Tabel Account_Guid
        public Guid AccountGuid { get; set; }
        [Column(name: "role guid")] // Membuat Column Tabel Role_Guid
        public Guid RoleGuid { get; set; }

        //Cardinality
        public Accounts? Accounts { get; set; }
        public Roles? Roles { get; set; }
    }
}
