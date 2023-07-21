using EventHub.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace EventHub.Jwt
{
    public class TokenManager : ITokenManager
    {
        private readonly SymmetricSecurityKey _key;

        public TokenManager(IConfiguration config)
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["JWT:secret"]));
        }

        public string GenerateToken(User user, string roleName)
        {
            //--claims
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,user.Email),
                new Claim(ClaimTypes.Role, roleName)
            };

            //--credentials
            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            //--tokenDescriptor
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            //--tokenHandler
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
