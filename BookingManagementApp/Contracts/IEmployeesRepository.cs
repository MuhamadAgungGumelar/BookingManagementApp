﻿using BookingManagementApp.Models;

namespace BookingManagementApp.Contracts
{
    public interface IEmployeesRepository : IGeneralRepository<Employees>
    { 
        string? GetLastNik();
        Employees? GetByEmail(string email);
    }
}