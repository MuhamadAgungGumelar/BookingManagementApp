using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;

namespace BookingManagementApp.Repositories
{
    //Class Employee Repository yang inheritence ke class GeneralRepository dan Interface Employees
    public class EmployeesRepostitory : GeneralRepository<Employees>, IEmployeesRepository
    {
        //Constructor
        public EmployeesRepostitory(BookingManagementDbContext context):base (context) { }

        //Membuat Method GetLastNik dari input users ke database
        public string? GetLastNik()
        {
            // Mengurutkan NIK pengguna secara menurun (descending),
            // dan mengambil NIK pertama dari hasil pengurutan (NIK terakhir dalam database)
            var lastNik = _context.Set<Employees>().OrderByDescending(x => x.Nik).Select(x => x.Nik).FirstOrDefault();
            return lastNik;
        }

        public Employees? GetByEmail(string email)
        {
            return _context.Set<Employees>().FirstOrDefault(x => x != null && x.Email == email);
        }
    }
}
