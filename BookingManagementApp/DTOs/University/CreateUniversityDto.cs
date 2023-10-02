using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.University;

    public class CreateUniversityDto
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public static implicit operator Universities(CreateUniversityDto createUniversityDto)
        {
            return new Universities
            {
                Code = createUniversityDto.Code,
                Name = createUniversityDto.Name,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }

