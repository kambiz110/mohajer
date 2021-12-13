using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Entity;
using Mohajer.Models;
using Mohajer.Useful.Static;

namespace Mohajer.Service.Nazarat.Command.AddNazar
{
    public interface IAddNazar
    {
        ResultDto AddNazarAsync(Mohajer.Entity.Nazarat nazarat, string ip);
    }
    public class AddNazar : IAddNazar
    {
        private readonly MohajerContext _context;

        public AddNazar(MohajerContext context)
        {
            _context = context;
        }

        public ResultDto AddNazarAsync(Entity.Nazarat nazarat, string ip)
        {
            var dt1 = DateTime.Now.ToDateConvertDateTime();
          
            if (!_context.Nazarats.Where(p => p.Ip== ip && p.AtInserted.Date==dt1.Date).Any())
            {
                nazarat.Ip = ip;
                nazarat.AtInserted = dt1;
                _context.Add(nazarat);
                var result = _context.SaveChanges();
                if (result > 0)
                {
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = "عملیات درج با موفقیت انجام گردید"
                    };
                }
                else
                {
                    return new ResultDto
                    {
                        IsSuccess = false,
                        Message = "عملیات درج نا موفق بود مقادیر وارد شده نادرست بود"
                    };
                }
            }
           
            return new ResultDto
            {
                IsSuccess = false,
                Message = "شما در روز فقط یک مورد می توانید نظر خود را پیرامون سایت درج نمائید"
            };
        }
    }
}
