using BookingManagementApp.DTOs.Account;
using BookingManagementApp.DTOs.AccountRole;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations.CreateValidator
{
    // Inheritence ke AbstractValidator untuk DTO Create
    public class CreateAccountRolesValidator : AbstractValidator<CreateAccountRoleDto>
    {
        // Membuat Constructor
        public CreateAccountRolesValidator()
        {
            //Validasi ID Account Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ar => ar.AccountGuid)
                .NotEmpty().WithMessage("ID Account Tidak Boleh Kosong");

            //Validasi ID Role Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ar => ar.RoleGuid)
               .NotEmpty().WithMessage("ID Role Tidak Boleh Kosong");
        }
    }
}
