using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Category.Dto;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Category.Query
{
    public interface IGetCategorySideBare
    {
        ResultDto<SideBareDto> Exequt();
    }
    public class GetCategorySideBare : IGetCategorySideBare
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;
        public GetCategorySideBare(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<SideBareDto> Exequt()
        {
            var categories = _mohajerContext.Categores.Where(p => p.ParentCategoryId == null && p.IsRemove == false).ToList();
            List<SideBarCategory> LstsideBars = new List<SideBarCategory>();
            if (categories.Count()>0)
            {
                foreach (var item in categories)
                {
                    var productCount = _mohajerContext.Prodoctes.Where(p => p.CategoryId == item.Id).Count();
                    LstsideBars.Add(new SideBarCategory {CategoryName=item.Title ,count= productCount , Id=item.Id });
                }
               
                SideBareDto sideBareDto = new SideBareDto();
                var tages = _mohajerContext.Tages.Select(p => p.Title).ToList();
                var TreeProduct = _mohajerContext.Prodoctes.OrderByDescending(p => p.Id).Take(3);
               
                var proViewModel = _mapper.Map<List<ProductViewModel>>(TreeProduct);
                sideBareDto.sideBarCategories = LstsideBars;
                sideBareDto.TagesSlidBare = tages;
                sideBareDto.products = proViewModel;
                return new ResultDto<SideBareDto>
                {
                    Data = sideBareDto,
                    IsSuccess = true,
                    Message = "عملیات موفق"
                };

            }

            else
            {
                return new ResultDto<SideBareDto> {
                Data=null,
                 IsSuccess=false,
                 Message="عملیات ناموفق"
                };
            }
        }
    }
}
