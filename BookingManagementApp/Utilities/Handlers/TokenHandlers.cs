using BookingManagementApp.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BookingManagementApp.Utilities.Handlers
{
    public class TokenHandlers : ITokenHandler
    {
        private readonly IConfiguration _configuration;

        public TokenHandlers (IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string Generate(IEnumerable<Claim> claims)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
            var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
                                                    audience: _configuration["JWTService:Audience"],
                                                    claims: claims,
                                                    expires: DateTime.Now.AddMinutes(5),
                                                    signingCredentials: sigingCredentials);
            var encodeToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
            return encodeToken;
        }
    }
}
