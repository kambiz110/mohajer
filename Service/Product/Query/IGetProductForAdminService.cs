using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Admin.Product.Query
{


    public class GetProductesForAdminService : IGetProductesForAdminService
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;
        public GetProductesForAdminService(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<ProductForAdminDto> Exequte(int PageSize, int PageNo, bool confimeted=false)
        {
            int rowCount = 0;

            List<Prodoct> result = new List<Prodoct>();

            result = _mohajerContext.Prodoctes.Where(p => p.IsRemove == false && p.IsActive==confimeted).ToPaged(PageNo, PageSize, out rowCount).ToList();



            if (result != null && result.Count() > 0)
            {
                var model = new ProductForAdminDto
                {
                    Products = _mapper.Map<List<ProductsFormAdminList_Dto>>(result),
                    PagerDto = new PagerDto
                    {
                        PageNo = PageNo,
                        PageSize = PageSize,
                        TotalRecords = rowCount
                    }
                };
                return new ResultDto<ProductForAdminDto>
                {

                    Data = model,
                    IsSuccess = true,
                    Message = "عملیات موفق بود"

                };
            }
            return new ResultDto<ProductForAdminDto>
            {

                Data = null,
                IsSuccess = false,
                Message = "عملیات موفق بود"

            };


        }
    }
}
