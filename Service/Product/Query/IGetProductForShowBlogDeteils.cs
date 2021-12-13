using AutoMapper;
using Mohajer.Entity;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Query
{
    public interface IGetProductForShowBlogDeteils
    {
        ResultDto<ProductForShowBlogDeteilsDto> Exequt(long id);
    }

    public class GetProductForShowBlogDeteils : IGetProductForShowBlogDeteils
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetProductForShowBlogDeteils(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<ProductForShowBlogDeteilsDto> Exequt(long id)
        {
            var product = _mohajerContext.Prodoctes
               .Where(p => p.Id == id && p.IsActive == true && p.IsRemove == false)
               .FirstOrDefault();
            if (product != null)
            {
                var result = _mapper.Map<ProductViewModel>(product);
                var commentes = _mohajerContext.Comments
                    .Where(p => p.ProdoctId == product.Id && p.IsActive == true && p.IsRemove == false)
                    .ToList();
                ProductForShowBlogDeteilsDto forShowBlogDeteilsDto = new ProductForShowBlogDeteilsDto
                {
                productViewModel= result ,
                comments= commentes ,
                Id=id
                };
                return new ResultDto<ProductForShowBlogDeteilsDto>
                {
                   Data= forShowBlogDeteilsDto ,
                   IsSuccess=true ,
                   Message="عملیات با موفقیت انجام گرفت"
                };


            }
            else
            {
                return new ResultDto<ProductForShowBlogDeteilsDto>
                {
                    Data = null,
                    IsSuccess = false,
                    Message = "عملیات  نا موفق بود"
                };
            }
            
        }
    }

    public class ProductForShowBlogDeteilsDto
    {
        public ProductViewModel productViewModel { get; set; }
        public List<Mohajer.Entity.Comment> comments { get; set; }
        public long Id { get; set; }

    }
}
