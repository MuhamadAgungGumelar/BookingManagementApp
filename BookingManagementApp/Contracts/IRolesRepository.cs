using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IRolesRepository: IGeneralRepository<Roles> 
    {
        public Guid? GetDefaultRoleGuid();
    }
}
