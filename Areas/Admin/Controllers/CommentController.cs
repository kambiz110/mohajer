using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Mohajer.Entity;
using Mohajer.Service.Comment.Query;
using Mohajer.Useful.Result;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CommentController : Controller
    {
        private readonly MohajerContext _context;
        private readonly IGetCommentAdmin _getCommentAdmin;
        public CommentController(MohajerContext context, IGetCommentAdmin getCommentAdmin)
        {
            _context = context;
            _getCommentAdmin = getCommentAdmin;
        }

        [HttpGet]
        public IActionResult ManegeComment(int PageSize = 5, int PageNo = 1)
        {
            var result = _getCommentAdmin.Excute(PageSize , PageNo , false).Data;
            return View(result);
        }
        [HttpGet]
        public IActionResult ConfirmetedComment(int PageSize = 5, int PageNo = 1)
        {
            var result = _getCommentAdmin.Excute(PageSize, PageNo, true).Data;

            return View(result);
        }

        [HttpGet]
        public IActionResult ChangeStatusComment(long Id)
        {
            var comment = _context.Comments.Find(Id);
            comment.IsActive = true;
            _context.Update(comment);
            var result = _context.SaveChanges();
            return RedirectToAction("ManegeComment");
        }
        [HttpGet]
        public IActionResult DeleteComment(long Id)
        {
            var comment = _context.Comments.Find(Id);
            comment.IsRemove = true;
            comment.Removed = DateTime.Now;
            _context.Update(comment);
            var result = _context.SaveChanges();
            return RedirectToAction("ManegeComment");
        }
        [HttpPost]
        public IActionResult AnswerComment(long Id ,string Comment)
        {
         var comment=   _context.Comments.Where(p => p.Id == Id).FirstOrDefault();
            if (comment!=null)
            {
                comment.AnserMsg = Comment;
                comment.IsActive = true;
                _context.SaveChanges();
            }
            return Json(new ResultDto
            {
                IsSuccess = true,
                Message = "موفق بود"
            });
        }
    }
}
