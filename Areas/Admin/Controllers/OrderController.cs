using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Mohajer.Entity;
using Ganss.XSS;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Service.Order.Command.AddOrder;
using Mohajer.Useful.Result;
using Mohajer.Service.Order.Query;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly MohajerContext _context;
        private readonly IDNTCaptchaValidatorService _validatorService;
        private readonly IAddOrder _getaddOrder;
        private readonly IGetOrder _getOrder;

        public OrderController(MohajerContext context, IAddOrder getaddOrder, IDNTCaptchaValidatorService validatorService, IGetOrder getOrder)
        {
            _context = context;
            _getaddOrder = getaddOrder;
            _validatorService = validatorService;
            _getOrder = getOrder;
        }
        [HttpGet]
        public IActionResult ManegeOrder(int PageSize = 5, int PageNo = 1 )
        {
          var result = _getOrder.Excute(PageSize, PageNo, false).Data;
            return View(result);
        }
        [HttpPost]
        [ValidateDNTCaptcha(
      ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
      CaptchaGeneratorLanguage = Language.Persian,
      CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbersToWords)]
        public IActionResult AddOrder(Order order)
        {

            if (ModelState.IsValid)
            {

                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                var ip = Request.HttpContext.Connection.RemoteIpAddress + "";
                //var testIP = "10,34,99,9";
                HtmlSanitizer sanitizer = new HtmlSanitizer();
                order.Phone = sanitizer.Sanitize(order.Phone);
                order.Full_Name = sanitizer.Sanitize(order.Full_Name);
                order.Description = sanitizer.Sanitize(order.Description);
                order.Subject = sanitizer.Sanitize(order.Subject);
                var result = _getaddOrder.AddOrderExequte(order, ip);

                return Json(result);
            }
            return Json(new ResultDto
            {
                IsSuccess = false,
                Message = "اعتبار سنجی ناموفق بود"
            });
        }

        [HttpGet]
        public IActionResult DeletOrder(long id)
        {
            var order = _context.Orders.Where(p => p.Id == id).FirstOrDefault();
            if (order != null)
            {

                order.IsRemove = true;
                order.Removed = DateTime.Now;
                _context.SaveChanges();
            }
            return RedirectToAction("ManegeOrder");
        }
    }
}
