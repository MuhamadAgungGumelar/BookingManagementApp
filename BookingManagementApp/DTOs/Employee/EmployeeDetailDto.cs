﻿using BookingManagementApp.Utilities.Enums;

namespace BookingManagementApp.DTOs.Employee
{
    public class EmployeeDetailDto
    {
        public Guid Guid { get; set; }
        public string Nik { get; set; }
        public string FullName { get; set; }
        public DateTime BirthDate { get; set; }
        public GenderLevel Gender { get; set; } 
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }  
        public string Major {  get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public string University { get; set; }

    }
}