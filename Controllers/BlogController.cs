using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.Mvc;
using Mohajer.Entity;
using Mohajer.Service.Product.Query;
using Mohajer.Useful.Result;
using Microsoft.AspNetCore.HttpOverrides;
using Mohajer.Service.Comment.Command;
using Ganss.XSS;

namespace Mohajer.Controllers
{
    public class BlogController : Controller
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IGetProductForEndPointUser _getProductForEndPointUser;
        private readonly IGetProductForShowBlogDeteils _getProductForShowBlogDeteils;
        private readonly IGetProductesWithCategoryId _getProductesWithCategoryId;
        private readonly IGetProductWithTag getProductWithTag;
        private readonly IGetProductWithMediaType _getProductWithMedia;
        private readonly IAddComment _addComment;

        public BlogController(IGetProductForEndPointUser getProductForEndPointUser,
            MohajerContext mohajerContext, IGetProductForShowBlogDeteils getProductForShowBlogDeteils,
            IGetProductesWithCategoryId getProductesWithCategoryId, IGetProductWithTag getProductWithTag,
            IGetProductWithMediaType getProductWithMedia, IAddComment addComment)
        {
            _getProductForEndPointUser = getProductForEndPointUser;
            _mohajerContext = mohajerContext;
            _getProductForShowBlogDeteils = getProductForShowBlogDeteils;
            _getProductesWithCategoryId = getProductesWithCategoryId;
            this.getProductWithTag = getProductWithTag;
            _getProductWithMedia = getProductWithMedia;
            _addComment = addComment;
        }

        public IActionResult BlogPage(int PageSize = 20, int PageNo = 1)
        {
            var result = _getProductForEndPointUser.Exequte(PageSize, PageNo).Data;
            return View(result);
        }

        [HttpGet]
        public IActionResult BlogDetail(long id)
        {
            var model = _getProductForShowBlogDeteils.Exequt(id);
            if (model.IsSuccess)
            {
                return View(model.Data);
            }
            return RedirectToAction("BlogPage");

        }
        [HttpGet]
        public IActionResult BlogCategory(long categoryId = 1, int PageSize = 10, int PageNo = 1)
        {

            var result = _getProductesWithCategoryId.Exequte(categoryId, PageSize, PageNo).Data;
            if (result == null)
            {
                return RedirectToAction("BlogPage");
            }
            return View(result);
        }
        [HttpGet]
        public IActionResult BlogQueryTag(string tag, int PageSize = 10, int PageNo = 1)
        {

            var result = getProductWithTag.Exequte(tag, PageSize, PageNo).Data;
            if (result == null)
            {
                return RedirectToAction("BlogPage");
            }
            return View(result);
        }

        [HttpGet]
        public IActionResult BlogMediaTag(int type, int PageSize = 10, int PageNo = 1)
        {

            var result = _getProductWithMedia.Exequte(type, PageSize, PageNo).Data;
            if (result == null)
            {
                return RedirectToAction("BlogPage");
            }
            return View(result);
        }


        [HttpPost]
        [ValidateDNTCaptcha(
            ErrorMessage = "عبارت امنیتی را به درستی وارد نمائید",
            CaptchaGeneratorLanguage = Language.Persian,
            CaptchaGeneratorDisplayMode = DisplayMode.SumOfTwoNumbersToWords)]
        public IActionResult AddComment(long ProductId ,[Bind("Id,Name,Email,CommentMsg,ProdoctId")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
                var ip = Request.HttpContext.Connection.RemoteIpAddress+"";
                //var testIP = "10,34,99,9";

                HtmlSanitizer sanitizer = new HtmlSanitizer();
                comment.Name = sanitizer.Sanitize(comment.Name);
                comment.Email = sanitizer.Sanitize(comment.Email);
                comment.CommentMsg = sanitizer.Sanitize(comment.CommentMsg);
                var result = _addComment.Exequte(comment , ip );
                return Json(result);
            }
            else
            {
                return Json(new ResultDto
                {
                    IsSuccess = false,
                    Message = "اعتبار سنجی ناموفق بود"
                });
            }
        }

    }
}
