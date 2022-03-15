using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace Service.TokensHandler
{
    public class TokenHandler
    {
        private IConfiguration _config;

        public TokenHandler(IConfiguration config)
        {
            _config = config;
        }

        public string GenerateJSONWebToken(string email)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[] { new Claim(JwtRegisteredClaimNames.Email, email) };


            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.UtcNow.AddDays(1),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public bool ValidateToken(IEnumerable<Claim> claims)
        {
            var result = false;

            try
            {
                var userClaims = claims.ToList();
                var expirationObj = userClaims.FirstOrDefault(z => z.Type == JwtRegisteredClaimNames.Exp)?.Value;
                if (string.IsNullOrWhiteSpace(expirationObj))
                    throw new Exception();

                var expirationDate = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                var exp = double.Parse(expirationObj);
                expirationDate = expirationDate.AddSeconds(exp).ToLocalTime();
                if (expirationDate <= DateTime.Now)
                    throw new Exception();

                var email = userClaims.FirstOrDefault(z => z.Type == ClaimTypes.Email)?.Value;
                if (string.IsNullOrWhiteSpace(email))
                    throw new Exception();

                result = true;
            }
            catch (Exception)
            {
                result = false;
            }

            return result;
        }

    }
}
