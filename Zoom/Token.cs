using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;

namespace Zoom
{
    public class Token
    {
        public JwtSecurityToken SecurityToken;
        public JwtSecurityTokenHandler SecurityTokenHandler;
        private string _token => SecurityTokenHandler.WriteToken(SecurityToken);

        public Token()
        {
            DateTime Expiry = DateTime.UtcNow.AddMinutes(20);

            int ts = (int)(Expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["ApiSecret"]));

            // length should be >256b
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            //Create a Token
            var header = new JwtHeader(credentials);

            //Zoom Required Payload
            var payload = new JwtPayload
            {
                { "iss", ConfigurationManager.AppSettings["ApiKey"]},
                { "exp", ts },
            };

            SecurityToken        = new JwtSecurityToken(header, payload);
            SecurityTokenHandler = new JwtSecurityTokenHandler();
        }

        public static implicit operator string(Token token) => token._token;
    }
}
