﻿using BookingManagementApp.DTOs.Booking;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations
{
    // Inheritence ke AbstractValidator untuk DTO Update dan Delete
    public class BookingsValidator : AbstractValidator<BookingDto>
    {
        // Constructor
        public BookingsValidator() 
        {
            // Validasi ID Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.Guid)
                .NotEmpty().WithMessage("ID Booking Tidak Boleh Kosong");

            // Validasi Tanggal Mulai Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.StartDate)
               .NotEmpty().WithMessage("Tanggal Mulai Booking Tidak Boleh Kosong");

            // Validasi Tanggal Akhir Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.EndDate)
               .NotEmpty().WithMessage("Tanggal Akhir Booking Tidak Boleh Kosong");

            // Validasi Status Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(b => b.Status).NotEmpty().WithMessage("Status Booking Tidak Boleh Kosong");

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