using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Room
{
    public class CreateRoomDto
    {
        public string Name { get; set; }
        public int Floor {  get; set; }
        public int Capacity { get; set; }

        public static implicit operator Rooms(CreateRoomDto createRoomDto)
        {
            return new Rooms
            {
                Name = createRoomDto.Name,
                Floor = createRoomDto.Floor,
                Capacity = createRoomDto.Capacity,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }   
}
