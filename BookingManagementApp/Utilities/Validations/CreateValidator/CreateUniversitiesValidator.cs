using BookingManagementApp.DTOs.University;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateUniversitiesValidator : AbstractValidator<CreateUniversityDto>
    {
        // Constructor
        public CreateUniversitiesValidator()
        {
            // Validasi Code Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(u => u.Code)
                .NotEmpty().WithMessage("Code Universitas Tidak Boleh Kosong");

            // Validasi Nama Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(u => u.Name)
               .NotEmpty().WithMessage("Nama Universitas Tidak Boleh Kosong");
        }
    }
}
