using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Order.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Order.Query
{
    public interface IGetOrder
    {
        ResultDto<LstOrderPagerDto> Excute(int PageSize, int PageNo, bool isConfim);
    }
    public class GetOrder : IGetOrder
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetOrder(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<LstOrderPagerDto> Excute(int PageSize, int PageNo, bool isConfim)
        {
            int rowCount = 0;
            var result = _mohajerContext.Orders.Where(p=> p.IsRemove == false).ToPaged(PageNo, PageSize, out rowCount).ToList();
           
            if (result!=null && result.Count()>0)
            {
                var model = _mapper.Map<List<OrderViewModel>>(result);
                PagerDto pag = new PagerDto
                {
                    PageNo = PageNo,
                    PageSize = PageSize,
                    TotalRecords = rowCount
                };
                var lstorderpager = new LstOrderPagerDto {
                    LSTOrderDtos = model ,
                    PagerDto=pag
                
                };
                return new ResultDto<LstOrderPagerDto>
                {

                    Data = lstorderpager,
                    IsSuccess = true,
                    Message = "موفق"
                };

            }

            return new ResultDto<LstOrderPagerDto>
            {

                Data = new LstOrderPagerDto(),
                IsSuccess = false,
                Message = "موردی یافت نگردید"
            };
        }
    }
}
