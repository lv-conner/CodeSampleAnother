using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace JWTWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class JWTCodeController : ControllerBase
    {
        private readonly RsaSecurityKey _rsaSecurityKey;
        public JWTCodeController(RsaSecurityKey rsaSecurityKey)
        {
            _rsaSecurityKey = rsaSecurityKey;
        }
        public ActionResult<string> PostCode(LoginModel loginModel)
        {
            IIdentity identity = new ClaimsIdentity("local");
            var user = new ClaimsIdentity(identity);
            user.AddClaim(new Claim("role", "admin"));
            user.AddClaim(new Claim("role", "user"));
            var signingCredentials = new SigningCredentials(_rsaSecurityKey, SecurityAlgorithms.RsaSha256Signature);
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = "http://localhost:5000",
                Audience = "api",
                SigningCredentials = signingCredentials,
                Subject = user
            };
            var securityToken = handler.CreateJwtSecurityToken(tokenDescriptor);
            var token = handler.WriteToken(securityToken);
            return token;
        }
    }

    public class LoginModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}