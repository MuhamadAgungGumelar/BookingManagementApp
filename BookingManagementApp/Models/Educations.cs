namespace BookingManagementApp.Models
{
    public class Educations : BaseEntity
    {
        public string Major { get; set; }
        public string Degree { get; set; }
        public Boolean Gpa { get; set; }
        public Guid UniversityGuid { get; set; }
    }
}
