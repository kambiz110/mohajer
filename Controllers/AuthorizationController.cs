using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

using DNTCaptcha.Core;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mohajer.Entity;
using Mohajer.Service.User.Dto;
using Mohajer.Service.User.Query;

namespace Mohajer.Controllers
{
    public class AuthorizationController : Controller
    {


      
        private readonly ILogger<AuthorizationController> _logger;
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public AuthorizationController(ILogger<AuthorizationController> logger, MohajerContext mohajerContext, IMapper mapper)
        {
            _logger = logger;

            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]

        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbersToWords)]

        public IActionResult Login(InputLoginDto dto, bool RemimberMy = false)
        {
            MyLogin _login = new MyLogin(_mohajerContext , _mapper);
            var result = _login.Execute(dto);
            if (result.Data!=null)
            {
                HttpContext.SignInAsync(result.Data.principal, result.Data.properties);
                return RedirectToAction("Index","Home");
            }
            
            return View(dto);
        }
        public IActionResult SignOut()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Index", "Home");
        }
    }
}
