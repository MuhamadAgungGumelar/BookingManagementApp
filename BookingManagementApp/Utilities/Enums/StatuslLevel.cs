using System.ComponentModel.DataAnnotations;

namespace BookingManagementApp.Utilities.Enums;
public enum StatuslLevel
{
    Requested,
    Approved,
    Rejected,
    Canceled,
    Completed,
    [Display(Name = "On Going")]OnGoing,
}

