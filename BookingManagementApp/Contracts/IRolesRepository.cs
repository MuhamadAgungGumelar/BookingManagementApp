using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IRolesRepository
    {
        IEnumerable<Roles> GetAll();
        Roles? GetByGuid(Guid guid);
        Roles? Create(Roles role);
        bool Update(Roles role);
        bool Delete(Guid guid);
    }
}
