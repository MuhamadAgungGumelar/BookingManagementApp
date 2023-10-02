using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IUniversitiesRepository
    {
        IEnumerable<Universities> GetAll();
        Universities? GetByGuid(Guid guid);
        Universities? Create(Universities universities);
        bool Update(Universities universities);
        bool Delete(Guid guid);
    }
}
