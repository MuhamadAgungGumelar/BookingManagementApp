using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class RoomsRepository : GeneralRepository<Rooms>, IRoomsRepository
    {
        public RoomsRepository(BookingManagementDbContext context): base(context) { }
    }
}
