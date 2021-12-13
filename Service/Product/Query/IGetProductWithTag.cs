using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Product.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Query
{
    public interface IGetProductWithTag
    {
        ResultDto<ProductForUserDto> Exequte(string tag, int PageSize, int PageNo);
    }
    public class GetProductWithTag : IGetProductWithTag
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetProductWithTag(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<ProductForUserDto> Exequte(string tag, int PageSize, int PageNo)
        {
            int rowCount = 0;

            List<Prodoct> result = new List<Prodoct>();

            result = _mohajerContext.Prodoctes.Where(p => p.IsRemove == false && p.IsActive == true && p.Tages.Contains(tag)).ToPaged(PageNo, PageSize, out rowCount).ToList();
            if (result != null && result.Count() > 0)
            {
                var Products = _mapper.Map<List<ProductsFormUserList_Dto>>(result);
                for (int i = 0; i < Products.Count; i++)
                {
                    var productCount = _mohajerContext.Comments.Where(p => p.ProdoctId == Products[i].Id && p.IsActive == true).Count();
                    Products[i].CommentCount = productCount;
                }
                var model = new ProductForUserDto
                {
                    Products = Products,
                    PagerDto = new PagerDto
                    {
                        PageNo = PageNo,
                        PageSize = PageSize,
                        TotalRecords = rowCount
                    }
                };
                return new ResultDto<ProductForUserDto>
                {

                    Data = model,
                    IsSuccess = true,
                    Message = "عملیات موفق بود"

                };
            }
            return new ResultDto<ProductForUserDto>
            {

                Data = null,
                IsSuccess = false,
                Message = "عملیات موفق بود"

            };
        }
    }
}
