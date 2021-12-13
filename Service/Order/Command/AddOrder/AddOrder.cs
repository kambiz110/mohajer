using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Entity;
using Mohajer.Models;
using Mohajer.Useful.Static;

namespace Mohajer.Service.Order.Command.AddOrder
{
    public interface IAddOrder
    {
        ResultDto AddOrderExequte(Mohajer.Entity.Order nazarat, string ip);
    }
    public class AddOrder : IAddOrder
    {
        private readonly MohajerContext _context;

        public AddOrder(MohajerContext context)
        {
            _context = context;
        }

        public ResultDto AddOrderExequte(Entity.Order order, string ip)
        {
            var dt1 = DateTime.Now.ToDateConvertDateTime();
          
            if (!_context.Orders.Where(p => p.Ip== ip && p.AtInserted.Date==dt1.Date).Any())
            {
                order.Ip = ip;
                order.AtInserted = dt1;
                order.IsRemove = false;
                _context.Add(order);
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
                Message = "شما در روز فقط یک مورد می توانید یک سفارش درج نمائید"
            };
        }
    }
}
