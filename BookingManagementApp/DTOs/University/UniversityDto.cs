using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.University
{
    public class UniversityDto
    {
        public Guid Guid { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }

        public static explicit operator UniversityDto(Universities university)
        {
            return new UniversityDto
            {
                Guid = university.Guid,
                Code = university.Code,
                Name = university.Name
            };
        }

        public static implicit operator Universities(UniversityDto universityDto)
        {
            return new Universities
            {
                Guid = universityDto.Guid,
                Code = universityDto.Code,
                Name = universityDto.Name,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
