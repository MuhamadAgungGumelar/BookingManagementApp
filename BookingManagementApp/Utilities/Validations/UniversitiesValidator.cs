using BookingManagementApp.DTOs.University;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations
{
    // Inheritence ke AbstractValidator untuk DTO Update dan Delete
    public class UniversitiesValidator : AbstractValidator<UniversityDto>
    {
        // Constructor
        public UniversitiesValidator() 
        {
            // Validasi ID Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(u => u.Guid)
                .NotEmpty().WithMessage("ID Universitas Tidak Boleh Kosong");

            // Validasi Code Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(u => u.Code)
                .NotEmpty().WithMessage("Code Universitas Tidak Boleh Kosong");

            // Validasi Nama Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(u => u.Name)
               .NotEmpty().WithMessage("Nama Universitas Tidak Boleh Kosong");
        }
    }
}
