using System.Collections.Generic;
using System.Security.Claims;

namespace Service.TokensHandler
{
    public interface ITokenHandler
    {
        string GenerateJSONWebToken(string email);
        bool ValidateToken(IEnumerable<Claim> claims);
    }
}
