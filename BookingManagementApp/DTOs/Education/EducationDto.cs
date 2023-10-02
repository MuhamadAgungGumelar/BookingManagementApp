using BookingManagementApp.DTOs.Employee;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Education
{
    public class EducationDto
    {
        public Guid Guid { get; set; }
        public string Major { get; set; }
        public string Degree { get; set; }
        public float Gpa { get; set; }
        public Guid UniversityGuid { get; set; }

        public static explicit operator EducationDto(Educations educations)
        {
            return new EducationDto
            {
                Guid = educations.Guid,
                Major = educations.Major,
                Degree = educations.Degree,
                Gpa = educations.Gpa,
                UniversityGuid = educations.UniversityGuid
            };
        }

        public static implicit operator Educations(EducationDto educationDto)
        {
            return new Educations
            {
                Guid= educationDto.Guid,
                Major = educationDto.Major,
                Degree= educationDto.Degree,
                Gpa= educationDto.Gpa,
                UniversityGuid = educationDto.UniversityGuid,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
