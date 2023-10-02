using BookingManagementApp.DTOs.Booking;
using BookingManagementApp.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BookingManagementApp.DTOs.Account
{
    public class CreateAccountDto
    {
        public Guid Guid { get; set; }
        public string Password { get; set; }
        public int OTP { get; set; }
        public bool IsUsed { get; set; }
        public DateTime ExpiredTime { get; set; }

        public static implicit operator Accounts(CreateAccountDto createAccountDto)
        {
            return new Accounts
            {
                Guid = createAccountDto.Guid,
                Password = createAccountDto.Password,
                OTP = createAccountDto.OTP,
                IsUsed = createAccountDto.IsUsed,
                ExpiredTime = createAccountDto.ExpiredTime,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
        }
    }
}
