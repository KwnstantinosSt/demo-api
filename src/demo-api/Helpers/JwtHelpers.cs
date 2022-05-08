using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using demo_api.Configs;
using demo_api.Models;
using Microsoft.IdentityModel.Tokens;

namespace demo_api.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this User userAccounts, int Id)
        {
            IEnumerable<Claim> claims = new Claim[] {
                    new Claim("Id", userAccounts.Id.ToString()),
                    new Claim(ClaimTypes.Name, userAccounts.Name),
                    new Claim(ClaimTypes.Email, userAccounts.Email),
                    new Claim(ClaimTypes.NameIdentifier, Id.ToString()),
                    new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyyy HH:mm:ss tt"))
            };
            return claims;
        }
        public static IEnumerable<Claim> GetClaims(this User userAccounts, out int Id)
        {
            Id = userAccounts.Id;
            return GetClaims(userAccounts, Id);
        }
        public static User GenTokenkey(User model, JwtSettings jwtSettings)
        {
            try
            {
                var UserToken = new User();
                if (model == null) return null;
                // Get secret key
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);
                int Id = model.Id;
                DateTime expireTime = DateTime.UtcNow.AddDays(1);
                UserToken.UserToken.Validaty = expireTime.TimeOfDay;
                var JWToken = new JwtSecurityToken(issuer: jwtSettings.ValidIssuer, audience: jwtSettings.ValidAudience, claims: GetClaims(model, out Id), notBefore: new DateTimeOffset(DateTime.Now).DateTime, expires: new DateTimeOffset(expireTime).DateTime, signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256));
                UserToken.UserToken.Token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                UserToken.Name = model.Name;
                UserToken.Id = model.Id;
                return UserToken;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}