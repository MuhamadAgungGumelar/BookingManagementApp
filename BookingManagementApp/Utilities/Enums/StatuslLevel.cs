using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Utilities.Enums;
public enum StatuslLevel // Membuat Tipe Data Enum Untuk StatusLevel
{
    Requested,
    Approved,
    Rejected,
    Canceled,
    Completed,
    [Display(Name = "On Going")]OnGoing,
}

