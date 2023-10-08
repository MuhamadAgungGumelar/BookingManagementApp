using System.Security.Claims;

namespace BookingManagementApp.Contracts
{
    public interface ITokenHandler
    {
        string Generate(IEnumerable<Claim> claims);
    }


}
