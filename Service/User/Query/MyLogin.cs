using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Service.Admin.User.Dto;
using Mohajer.Service.User.Dto;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Mohajer.Service.User.Query
{
    public interface IMyLogin
    {
        ResultDto<ClaimDto> Execute(InputLoginDto dto);
    }

    public class MyLogin : IMyLogin
    {

        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public MyLogin(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<ClaimDto> Execute(InputLoginDto dto)
        {
            var user = _mohajerContext.Users.Where(p => p.Email == dto.Email).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == dto.Password.Trim())
                {
                    var result = _mapper.Map<LoginUserDto>(user);
                    var resultClime = Autentic(result);
                    user.Token = resultClime.JwtSecurityToken.EncodedPayload.ToString();
                    _mohajerContext.Update(user);
                    _mohajerContext.SaveChanges();
                    return new ResultDto<ClaimDto>
                    {

                        Data = resultClime,
                        IsSuccess = false,
                        Message = "عملیات موفق بود"
                    };
                }
                else
                {
                    return new ResultDto<ClaimDto>
                    {

                        Data = null,
                        IsSuccess = false,
                        Message = "کلمه عبور وارد شده اشتباه می باشد."
                    };
                }
            }
            return new ResultDto<ClaimDto>
            {

                Data = null,
                IsSuccess = false,
                Message = "کاربر با ایمیل وارد شده موجود نمی باشد."
            };

        }
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
            Random rnd = new Random();
            int rrr = rnd.Next(5, 13);
            string keyValue = RandomString(rrr);


            var key = Encoding.ASCII.GetBytes(keyValue);
            //Generate Token for user - JRozario
            JwtSecurityToken JWToken = new JwtSecurityToken(
                issuer: "http://localhost:45092/",
                audience: "http://localhost:45092/",
                claims: claims,
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
                JwtSecurityToken = JWToken,
                principal = principal,
                properties = properties,
            };
        }
        private Random random = new Random();
        public string RandomString(int length)
        {

            const string chars = "ABCDEFGHIJKLMNOPQR@#$%&!STUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
           .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
