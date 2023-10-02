using BookingManagementApp.DTOs.University;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Room
{
    public class RoomDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public int Floor { get; set; }
        public int Capacity { get; set; }

        public static explicit operator RoomDto(Rooms room)
        {
            return new RoomDto
            {
                Guid = room.Guid,
                Name = room.Name,
                Floor = room.Floor,
                Capacity = room.Capacity
            };
        }

        public static implicit operator Rooms(RoomDto roomDto)
        {
            return new Rooms
            {
                Guid = roomDto.Guid,
                Name = roomDto.Name,
                Floor = roomDto.Floor,
                Capacity = roomDto.Capacity,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
