using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IAccountRolesRepository
    {
        IEnumerable<AccountRoles> GetAll();
        AccountRoles? GetByGuid(Guid guid);
        AccountRoles? Create(AccountRoles accountRole);
        bool Update(AccountRoles accountRole);
        bool Delete(Guid guid);
    }
}
