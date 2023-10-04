using BookingManagementApp.DTOs.Room;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateRoomsValidator : AbstractValidator<CreateRoomDto>
    {
        // Constructor
        public CreateRoomsValidator()
        {
            // Validasi Nama Ruangan Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(r => r.Name)
                .NotEmpty().WithMessage("Nama Ruangan Tidak Boleh Kosong");

            // Validasi Urutan Lantai Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Urutan Lantai Harus Angka dan Menampilkan Pesan
            RuleFor(r => r.Floor)
               .NotEmpty().WithMessage("Urutan Lantai Tidak Boleh Kosong")
               .IsInEnum().WithMessage("Harus Berisi Angka");

            // Validasi Capasitas Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi Capasitas Harus Angka dan Menampilkan Pesan
            RuleFor(r => r.Capacity)
               .NotEmpty().WithMessage("Kapasitas Ruangan Tidak Boleh Kosong")
               .IsInEnum().WithMessage("Harus Berisi Angka");
        }
    }
}
