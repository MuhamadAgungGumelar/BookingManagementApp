using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class RolesRepository : GeneralRepository<Roles>, IRolesRepository
    { 
        public RolesRepository(BookingManagementDbContext context): base(context) { }
    }
}
