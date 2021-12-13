using Mohajer.Entity;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Useful.Static;

namespace Mohajer.Service.Comment.Command
{
    public interface IAddComment
    {
        ResultDto Exequte(Mohajer.Entity.Comment comment, string ip);
    }
    public class AddComment : IAddComment
    {
        private readonly MohajerContext _context;

        public AddComment(MohajerContext context)
        {
            _context = context;
        }

        public ResultDto Exequte(Entity.Comment comment, string ip )
        {

            var dt = DateTime.Now.ToDateConvertDateTime();
            var dt2 = _context.Comments.Where(p => p.ProdoctId == comment.ProdoctId && p.Ip == ip).OrderByDescending(p=>p.AtInserted).FirstOrDefault();
            if (dt2!=null)
            {
                var equlseDate = dt2.AtInserted.TwoDateEqules(dt);
                if (_context.Comments.Where(p => p.ProdoctId == comment.ProdoctId && p.Ip == ip).Any() && !equlseDate)
                {

                    comment.Ip = ip;
                    comment.AtInserted = dt;
                    _context.Add(comment);
                    var result = _context.SaveChanges();
                    if (result > 0)
                    {
                        return new ResultDto
                        {
                            IsSuccess = true,
                            Message = "عملیات درج با موفقیت انجام گردید"
                        };
                    }
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عملیات درج نا موفق بود مقادیر وارد شده نادرست بود"
                    };
                }
            }
            else
            {
                comment.Ip = ip;
                comment.AtInserted = dt;
                _context.Add(comment);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = "عملیات درج با موفقیت انجام گردید"
                    };
                }
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات درج نا موفق بود مقادیر وارد شده نادرست بود"
                };
            }
          
            
            return new ResultDto
            {
                IsSuccess = false,
                Message = "شما برای این محتوا در امروز فقط یک پیام می توانید ثبت کنید"
            };
        }
    }
}
