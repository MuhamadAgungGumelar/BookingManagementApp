using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IRoomsRepository
    {
        IEnumerable<Rooms> GetAll();
        Rooms? GetByGuid(Guid guid);
        Rooms? Create(Rooms room);
        bool Update(Rooms room);
        bool Delete(Guid guid);
    }
}
