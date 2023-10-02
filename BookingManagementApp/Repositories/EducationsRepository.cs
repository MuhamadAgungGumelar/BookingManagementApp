using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using static BookingManagementApp.Repositories.EducationsRepository;

namespace BookingManagementApp.Repositories
{
    public class EducationsRepository : GeneralRepository<Educations>, IEducationsRepository
    {
        public EducationsRepository(BookingManagementDbContext context) : base(context) { }

    }
}
