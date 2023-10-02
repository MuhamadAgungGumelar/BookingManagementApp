using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Models;

namespace BookingManagementApp.DTOs.Account
{
    public class AccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static explicit operator AccountDto(Accounts accounts)
        {
            return new AccountDto
            {
                Guid = accounts.Guid,
                Password = accounts.Password,
                OTP = accounts.OTP,
                IsUsed = accounts.IsUsed,
                ExpiredTime = accounts.ExpiredTime
            };
        }

        public static implicit operator Accounts(AccountDto accountDto)
        {
            return new Accounts
            {
                Guid = accountDto.Guid,
                Password = accountDto.Password,
                OTP = accountDto.OTP,
                IsUsed = accountDto.IsUsed,
                ExpiredTime = accountDto.ExpiredTime,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
