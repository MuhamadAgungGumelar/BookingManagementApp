using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IEducationsRepository
    {
        IEnumerable<Educations> GetAll();
        Educations? GetByGuid(Guid guid);
        Educations? Create(Educations education);
        bool Update(Educations education);
        bool Delete(Guid guid);
    }
}
