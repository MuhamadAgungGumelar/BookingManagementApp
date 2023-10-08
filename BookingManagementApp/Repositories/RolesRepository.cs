using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class RolesRepository : GeneralRepository<Roles>, IRolesRepository
    { 
        public RolesRepository(BookingManagementDbContext context): base(context) { }

        public Guid? GetDefaultRoleGuid ()
        {
            // Cari peran default berdasarkan namanya
            return _context.Set<Roles>().FirstOrDefault(rl => rl.Name == "User")?.Guid;
        }
    }
}
