using Mohajer.Entity;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Command.UpdateProduct
{
    public interface IFindProductService
    {
        ResultDto<Prodoct> FindProduct(long Id);
    }
    public class FindProductService : IFindProductService
    {
        private readonly MohajerContext _mohajerContext;

        public FindProductService(MohajerContext mohajerContext)
        {
            this._mohajerContext = mohajerContext;
        }

        public ResultDto<Prodoct> FindProduct(long Id)
        {
            var product = _mohajerContext.Prodoctes.Where(p => p.Id == Id).FirstOrDefault();
            if (product != null && product.Id > 0)
            {
                return new ResultDto<Prodoct>
                {
                    Data = product,
                    IsSuccess = true,
                    Message = "عملیات موفق بود"
                };
            }
            return new ResultDto<Prodoct>
            {
                Data = null,
                IsSuccess = false,
                Message = "عملیات ناموفق بود"
            };
        }
    }
}
