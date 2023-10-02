using BookingManagementApp.DTOs.Room;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Role
{
    public class RoleDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }

        public static explicit operator RoleDto(Roles role)
        {
            return new RoleDto
            {
                Guid = role.Guid,
                Name = role.Name
            };
        }

        public static implicit operator Roles(RoleDto roleDto)
        {
            return new Roles
            {
                Guid = roleDto.Guid,
                Name = roleDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
