using BookingManagementApp.DTOs.Role;
using BookingManagementApp.Models;
using BookingManagementApp.Utilities.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.DTOs.Employee
{
    public class CreateEmployeeDto
    {
        public string FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public static implicit operator Employees(CreateEmployeeDto createEmployeeDto)
        {
            return new Employees
            {
                FirstName = createEmployeeDto.FirstName,
                LastName = createEmployeeDto.LastName, 
                BirthDate = createEmployeeDto.BirthDate,
                Gender = createEmployeeDto.Gender,
                HiringDate = createEmployeeDto.HiringDate,
                Email = createEmployeeDto.Email,
                PhoneNumber = createEmployeeDto.PhoneNumber,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
