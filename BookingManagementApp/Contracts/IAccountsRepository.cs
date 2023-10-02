using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IAccountsRepository
    {
        IEnumerable<Accounts> GetAll();
        Accounts? GetByGuid(Guid guid);
        Accounts? Create(Accounts account);
        bool Update(Accounts account);
        bool Delete(Guid guid);
    }
}
