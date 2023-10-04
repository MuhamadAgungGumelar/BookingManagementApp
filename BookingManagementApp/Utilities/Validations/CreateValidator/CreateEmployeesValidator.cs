using BookingManagementApp.DTOs.Employee;
using FluentValidation;
namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateEmployeesValidator : AbstractValidator<CreateEmployeeDto>
    {
        // Constructor
        public CreateEmployeesValidator()
        {
            // Validasi Nama Bagian Pertama Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(e => e.FirstName)
                .NotEmpty().WithMessage("Nama Bagian Pertama Tidak Boleh Kosong");

            // Validasi Tanggal Lahir Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Umur Minimal 18 Tahun dan Menampilkan Pesan
            RuleFor(e => e.BirthDate)
               .NotEmpty().WithMessage("Tanggal Lahir Tidak Boleh Kosong")
               .LessThanOrEqualTo(DateTime.Now.AddYears(-18)).WithMessage("Minimal Usia 18 Tahun"); // 18 years old

            // Validasi Gender Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Gender Harus Angka dan Menampilkan Pesan
            RuleFor(e => e.Gender)
               .NotNull().WithMessage("Gender Tidak Boleh Kosong")
               .IsInEnum().WithMessage("Harus Berisi Angka");

            // Validasi Tanggal Bekerja Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(e => e.HiringDate).NotEmpty().WithMessage("Tanggal Bekerja Tidak Boleh Kosong");

            // Validasi Email Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Format Email Harus Sesuai dan Menampilkan Pesan
            RuleFor(e => e.Email)
               .NotEmpty().WithMessage("Email Tidak Boleh Kosong")
               .EmailAddress().WithMessage("Format Email Salah");

            // Validasi Nomer Handphone Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Nomer Handphone Tidak Boleh Dibawah 10 Digit dan Menampilkan Pesan
            // Validasi Nomer Handphone Tidak Boleh Diatas 20 Digit dan Menampilkan Pesan
            RuleFor(e => e.PhoneNumber)
               .NotEmpty().WithMessage("Nomer Handphone Tidak Boleh Kosong")
               .MinimumLength(10).WithMessage("Nomer Handphone Tidak Boleh Dibawah 10 Digit")
               .MaximumLength(20).WithMessage("Nomer Handphone Tidak Boleh Diatas 20 Digit");
        }
    }
}
