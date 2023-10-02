using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    public class EmployeesRepostitory : GeneralRepository<Employees>, IEmployeesRepository
    {
        public EmployeesRepostitory(BookingManagementDbContext context):base (context) { }
    }
}
