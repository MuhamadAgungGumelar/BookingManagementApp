using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IAccountsRepository: IGeneralRepository<Accounts> 
    {
        Accounts? GetByEmail(string email);

        Universities? GetByUniversityName(string name);
    }
}
