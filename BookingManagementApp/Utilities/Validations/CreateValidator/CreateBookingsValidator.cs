using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Utilities.Enums;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateBookingsValidator : AbstractValidator<CreateBookingDto>
    {
        // Constructor
        public CreateBookingsValidator()
        {
            // Validasi Tanggal Mulai Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.StartDate)
               .NotEmpty().WithMessage("Tanggal Mulai Booking Tidak Boleh Kosong");

            // Validasi Tanggal Akhir Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.EndDate)
               .NotEmpty().WithMessage("Tanggal Akhir Booking Tidak Boleh Kosong");

            RuleFor(b => b.Status)
                .NotNull().WithMessage("Status Booking Tidak Boleh Kosong")
                .IsInEnum().WithMessage("Harus Berisi Angka yang Valid")
                .Must(status => Enum.IsDefined(typeof(StatuslLevel), status))
                .WithMessage("Harus Berisi Status yang Valid");

            // Validasi Keterangan Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.Remarks).NotEmpty().WithMessage("Keterangan Tidak Boleh Kosong");

            // Validasi ID Room Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.RoomGuid)
               .NotEmpty().WithMessage("ID Room Tidak Boleh Kosong");

            // Validasi ID Pegawai Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.EmployeeGuid)
               .NotEmpty().WithMessage("ID Pegawai Tidak Boleh Kosong");
        }
    }
}
