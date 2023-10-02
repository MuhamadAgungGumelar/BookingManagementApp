using BookingManagementApp.Contracts;
using BookingManagementApp.Data;
using BookingManagementApp.Models;
using System;

namespace BookingManagementApp.Repositories
{
    public class UniversitiesRepository : GeneralRepository<Universities>, IUniversitiesRepository
    {
        public UniversitiesRepository(BookingManagementDbContext context) : base(context) { }
    }
}
