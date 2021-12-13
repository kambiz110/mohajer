using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Service.Admin.User.Dto;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mohajer
{
    public class ClaimProvider
    {/// <summary>
     /// مقدار دهی برای احراز هویت با استفاده از claim
     /// </summary>
     /// <param name="role"></param>
     /// <param name="userId"></param>
     /// <param name="name"></param>
     /// <param name="family"></param>
     /// <param name="email"></param>
     /// <param name="phone"></param>
     /// <returns></returns>
        public ClaimDto Autentic(LoginUserDto model)
        {

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier,model.Id),
                new Claim(ClaimTypes.Email, model.Email),
                new Claim(ClaimTypes.Name, model.First_Name+" "+model.Last_Name),
                new Claim(ClaimTypes.Role, model.Role),
                new Claim(ClaimTypes.MobilePhone, model.Phone),
            };
            var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
            //Generate Token for user - JRozario
            JwtSecurityToken JWToken = new JwtSecurityToken(
                issuer: "http://localhost:45092/",
                audience: "http://localhost:45092/",
                claims: null,
                notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                expires: new DateTimeOffset(DateTime.Now.AddHours(1.0)).DateTime,
                //Using HS256 Algorithm to encrypt Token - JRozario
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            );


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var properties = new AuthenticationProperties()
            {
                IsPersistent = true
            };
            return new ClaimDto()
            {
                JwtSecurityToken= JWToken,
                principal = principal,
                properties = properties,
            };

            //HttpContext.SignInAsync(principal, properties);

        }
    }
}
