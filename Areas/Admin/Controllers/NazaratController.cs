using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTCaptcha.Core;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Mohajer.Entity;
using Mohajer.Models;
using Mohajer.Service.Nazarat.Command.AddNazar;
using Mohajer.Service.Nazarat.Query;
using Mohajer.Useful.Result;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NazaratController : Controller
    {
        private readonly MohajerContext _context;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly IAddNazar _addNazar;
        private readonly IGetNazarat _getNazar;

        public NazaratController(MohajerContext context, IDNTCaptchaValidatorService validatorService, IAddNazar addNazar, IGetNazarat getNazar)
        {
            _context = context;
            _validatorService = validatorService;
            _addNazar = addNazar;
            _getNazar = getNazar;
        }

        [HttpGet]
        public ActionResult ManegeNazarat(int PageSize = 5, int PageNo = 1)
        {
            var result = _getNazar.Excute(PageSize, PageNo , false).Data;

            return View(result);
        }

        [HttpGet]
        public ActionResult ConfirmetedNazarat(int PageSize = 5, int PageNo = 1)
        {
            var result = _getNazar.Excute(PageSize, PageNo, true).Data;

            return View(result);
        }
        [HttpGet]
        public ActionResult ChangeStatusNazar(long Id)
        {
            var nazar = _context.Nazarats.Find(Id);
            nazar.IsActive = true;
            _context.Update(nazar);
            var result = _context.SaveChanges();
            return RedirectToAction("ManegeNazarat");
        }
        [HttpGet]
        public ActionResult DeleteNazar(long Id)
        {
            var nazar = _context.Nazarats.Find(Id);
            nazar.IsRemove = true;
            nazar.Removed = DateTime.Now;
            _context.Update(nazar);
            var result = _context.SaveChanges();
            return RedirectToAction("ManegeNazarat");
        }

        [HttpPost]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbersToWords)]
        public IActionResult AddNazar(Nazarat nazarat)
        {

            if (ModelState.IsValid)
            {

                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                var ip = Request.HttpContext.Connection.RemoteIpAddress + "";
                //var testIP = "10,34,99,9";
                HtmlSanitizer sanitizer = new HtmlSanitizer();
                nazarat.Email = sanitizer.Sanitize(nazarat.Email);
                nazarat.Name = sanitizer.Sanitize(nazarat.Name);
                nazarat.Comment = sanitizer.Sanitize(nazarat.Comment);
                nazarat.Subject = sanitizer.Sanitize(nazarat.Subject);
                var result = _addNazar.AddNazarAsync(nazarat, ip);

                return Json(result);
            }
            return Json(new ResultDto
            {
                IsSuccess = false,
                Message = "اعتبار سنجی ناموفق بود"
            });
        }
    }
}
