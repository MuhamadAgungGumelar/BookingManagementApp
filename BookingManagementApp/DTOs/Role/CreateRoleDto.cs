using BookingManagementApp.DTOs.Room;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Role
{
    public class CreateRoleDto
    {
        public string Name { get; set; }

        public static implicit operator Roles(CreateRoleDto createRoleDto)
        {
            return new Roles
            {
                Name = createRoleDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
