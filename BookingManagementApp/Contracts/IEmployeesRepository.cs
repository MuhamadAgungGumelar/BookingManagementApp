using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IEmployeesRepository
    {
        IEnumerable<Employees> GetAll();
        Employees? GetByGuid(Guid guid);
        Employees? Create(Employees employee);
        bool Update(Employees employee);
        bool Delete(Guid guid);
    }
}
