using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Threading.Tasks;
using CodeSampleWebApp.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CodeSampleWebApp.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IEnumerable<Person> _persons;
        public  TokenController(IEnumerable<Person> persons)
        {
            _persons = persons;
        }
        [HttpPost]
        [Route("{userId}/{password}")]
        public IActionResult PostToken(string userId,string password)
        {
            var p = new Person();
            return Content(GenerateToken(p));
        }

        private string GenerateToken(Person p)
        {
            IIdentity identity = new ClaimsIdentity("local");
            var user = new ClaimsIdentity(identity);
            user.AddClaim(new Claim("role", "admin"));
            user.AddClaim(new Claim("role", "user"));

            var rsa = new RSACryptoServiceProvider(2048);
            var key = new RsaSecurityKey(rsa.ExportParameters(true));
            var signingCredentials = new SigningCredentials(key, SecurityAlgorithms.RsaSha256Signature);
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


}