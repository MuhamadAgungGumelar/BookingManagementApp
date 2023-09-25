namespace BookingManagementApp.Models
{
    public class AccountRoles : BaseEntity
    {
        public Guid AccountGuid { get; set; }
        public Guid RoleGuid { get; set; }
    }
}
