using BookingManagementApp.DTOs.Role;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateRolesValidator : AbstractValidator<CreateRoleDto>
    {
        // Constructor
        public CreateRolesValidator()
        {
            // Validasi Nama Role Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(rl => rl.Name)
                .NotEmpty().WithMessage("Nama Role Tidak Boleh Kosong");
        }
    }
}
