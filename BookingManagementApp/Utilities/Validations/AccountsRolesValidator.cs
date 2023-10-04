using BookingManagementApp.DTOs.AccountRole;
using FluentValidation;

namespace BookingManagementApp.Utilities.Validations
{
    // Inheritence ke AbstractValidator untuk DTO Update dan Delete
    public class AccountsRolesValidator : AbstractValidator<AccountRoleDto>
    {
        // Membuat Constructor
        public AccountsRolesValidator() 
        {
            // Validasi ID Pegawai Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ar => ar.Guid)
                .NotEmpty().WithMessage("ID Pegawai Tidak Boleh Kosong");

            //Validasi ID Account Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ar => ar.AccountGuid)
                .NotEmpty().WithMessage("ID Account Tidak Boleh Kosong");

            //Validasi ID Role Tidak Boleh Kosong dan Menampilkan Pesan
            RuleFor(ar => ar.RoleGuid)
               .NotEmpty().WithMessage("ID Role Tidak Boleh Kosong");
        }
    }
}
