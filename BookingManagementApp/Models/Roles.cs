﻿using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.Models
{
    [Table(name: "tb_m_roles")]
    public class Roles : BaseEntity
    {
        [Column(name: "name", TypeName = "nvarchar(100)")]
        public string Name { get; set; }
    }
}
