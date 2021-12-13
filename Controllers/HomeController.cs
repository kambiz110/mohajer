using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Mohajer.Entity;
using Mohajer.Models;

namespace Mohajer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MohajerContext _mohajerContext;
        public HomeController(ILogger<HomeController> logger , MohajerContext mohajerContext)
        {
            this._mohajerContext = mohajerContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult LoginUser(User user)
        {
            TokenProvider _tokenProvider = new TokenProvider(_mohajerContext);
           
            var userToken = _tokenProvider.LoginUser(user.Id, user.Password.Trim());
            if (userToken != null)
            {
                //Save token in session object
                HttpContext.Session.SetString("JWToken", userToken);
            }
            return Redirect("~/Home/Index");
        }
       public IActionResult Aboute()
        {
            return View();
        }

        public IActionResult Logoff()
        {
            HttpContext.Session.Clear();
            return Redirect("~/Home/Index");

        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Page_Aboutus() {
            return View();
        }
        public IActionResult Page_Contacts() {

            return View();
        }
      
    }
}
