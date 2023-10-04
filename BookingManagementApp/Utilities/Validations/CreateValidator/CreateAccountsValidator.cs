﻿using BookingManagementApp.DTOs.Account;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateAccountsValidator : AbstractValidator<CreateAccountDto>
    {
        // Membuat Constructor
        public CreateAccountsValidator()
        {
            // Validasi ID Pegawai Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(a => a.Guid)
                .NotEmpty().WithMessage("ID Pegawai Tidak Boleh Kosong");

            // Validasi Password Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(a => a.Password)
               .NotEmpty().WithMessage("Password Tidak Boleh Kosong");

            // Validasi OTP Tidak Boleh Kosong dan Menampilkan Pesan
            // Validasi OTP Harus Angka dan Menampilkan Pesan
            RuleFor(a => a.OTP)
               .NotEmpty().WithMessage("OTP Tidak Boleh Kosong")
               .IsInEnum().WithMessage("Harus Berisi Angka");

            // Validasi Status Booking Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(a => a.IsUsed).NotEmpty().WithMessage("Status Booking Tidak Boleh Kosong");

            // Validasi Masa Berlaku Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(a => a.ExpiredTime)
               .NotEmpty().WithMessage("Masa Berlaku Tidak Boleh Kosong");
        }
    }
}
