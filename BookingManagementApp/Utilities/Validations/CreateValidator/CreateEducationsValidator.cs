using BookingManagementApp.DTOs.Education;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateEducationsValidator : AbstractValidator<CreateEducationDto>
    {
        //Constructor
        public CreateEducationsValidator()
        {
            // Validasi ID Pegawai Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ed => ed.Guid)
                .NotEmpty().WithMessage("ID Pegawai Tidak Boleh Kosong");

            // Validasi Jurusan Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ed => ed.Major)
               .NotEmpty().WithMessage("Jurusan Tidak Boleh Kosong");

            // Validasi Gelar Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ed => ed.Degree)
               .NotEmpty().WithMessage("Gelar Tidak Boleh Kosong");

            // Validasi IPK Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi IPK Harus Angka dan Menampilkan Pesan
            RuleFor(ed => ed.Gpa).NotEmpty().WithMessage("IPK Tidak Boleh Kosong")
                .IsInEnum().WithMessage("Harus Berisi Angka");

            // Validasi ID Universitas Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ed => ed.UniversityGuid)
                .NotEmpty().WithMessage("ID UniversitasTidak Boleh Kosong");
        }
    }
}
