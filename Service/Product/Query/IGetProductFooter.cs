using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Category.Dto;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public interface IGetProductFooter
    {
        ResultDto<List<ProductViewModel>> Exequte();
    }
    public class GetProductFooter : IGetProductFooter
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetProductFooter(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<List<ProductViewModel>> Exequte()
        {
            var TreeProduct = _mohajerContext.Prodoctes.OrderByDescending(p => p.Id).Take(3);
            if (TreeProduct!=null)
            {
                var proViewModel = _mapper.Map<List<ProductViewModel>>(TreeProduct);
                return new ResultDto<List<ProductViewModel>>
                {
                    Data = proViewModel,
                    IsSuccess = true,
                    Message = "عملیات موفق"
                };

            }
            else
            {
                return new ResultDto<List<ProductViewModel>>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "عملیات ناموفق"
                };
            }
        }
    }
}
