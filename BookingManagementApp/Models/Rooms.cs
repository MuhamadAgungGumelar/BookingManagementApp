﻿namespace BookingManagementApp.Models
{
    public class Rooms : BaseEntity
    {
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }
    }
}
