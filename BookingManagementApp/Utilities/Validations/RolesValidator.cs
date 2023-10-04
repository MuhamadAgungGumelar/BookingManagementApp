using BookingManagementApp.DTOs.Role;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations
{
    // Inheritence ke AbstractValidator untuk DTO Update dan Delete
    public class RolesValidator : AbstractValidator <RoleDto>
    {
        // Constructor
        public RolesValidator() 
        {
            // Validasi ID Role Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(rl => rl.Guid)
                .NotEmpty().WithMessage("ID Role Tidak Boleh Kosong");

            // Validasi Nama Role Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(rl => rl.Name)
                .NotEmpty().WithMessage("Nama Role Tidak Boleh Kosong");
        }
    }
}
