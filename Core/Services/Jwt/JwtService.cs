using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.Jwt
{
    public class JwtService
    {
        private string securityKey = "";

        public string Generate(string id)
        {
            var symmetrycSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
            ///
            var credentials = new SigningCredentials(symmetrycSecurityKey, SecurityAlgorithms.HmacSha256Signature);
            ///
            var header = new JwtHeader(credentials);
            ///
            var payload = new JwtPayload(id, null, null, null, DateTime.Today.AddDays(1));
            ///
            var securityToken = new JwtSecurityToken(header, payload);

            return new JwtSecurityTokenHandler().WriteToken(securityToken);
        }

        public JwtSecurityToken Verify(string jwt)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            ///
            var keyEncoding = Encoding.ASCII.GetBytes(securityKey);
            ///
            tokenHandler.ValidateToken(jwt, new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(keyEncoding),
                ValidateIssuerSigningKey = true,
                ValidateIssuer = false,
                ValidateAudience = false
            }, out SecurityToken validateToken);
            ///
            return (JwtSecurityToken)validateToken;
        }
        // Example Generate
        //    var jwt = _jwtService.Generate(user.Id);
        //    Response.Cookies.Append("jwt", jwt, new CookieOptions
        //    {
        //       HttpOnly = true
        //    });

        // Example verify
        //    jwt = Request.Cookies["jwt"];
        //    var token = _jwtService.Verify(jwt);
        //    var userId = int.Parse(token.Issuer);
        //    var user = _userRepository.GetUserById(userId);
    }
}
