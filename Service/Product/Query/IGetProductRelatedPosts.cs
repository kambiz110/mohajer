using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Product.Dto;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Useful.Static;
using Mohajer.Useful.Pager;

namespace Mohajer.Service.Product.Query
{/// <summary>
/// نمایش پست های مرتبط با تعداد دلخواه
/// </summary>
    public interface IGetProductRelatedPosts
    {
        ResultDto<ProductForUserDto> Exequte(long Id, int cont);
    }

    /// <summary>
    /// نمایش پست های مرتبط با تعداد دلخواه
    /// </summary>
    public class GetProductRelatedPosts : IGetProductRelatedPosts
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetProductRelatedPosts(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<ProductForUserDto> Exequte(long Id, int cont)
        {

            List<Prodoct> result = new List<Prodoct>();
            var product = _mohajerContext.Prodoctes.Where(p => p.Id == Id).FirstOrDefault();
            var tages = product.Tages.ToArryTag();
            if (tages.Count()==1)
            {
                result = _mohajerContext.Prodoctes.Where(p => p.IsRemove == false && p.IsActive == true && p.Tages.Contains(tages[0]) && p.Id != Id).ToList();
            }
            else
            {
                foreach (var item in tages)
                {
                   var QueryWithTage = _mohajerContext.Prodoctes.Where(p => p.IsRemove == false && p.IsActive == true && p.Tages.Contains(item) && p.Id !=Id).ToList();
                    if (QueryWithTage.Count()>1)
                    {
                        foreach (var item2 in QueryWithTage)
                        {
                            if (result.Where(p=>p.Id==item2.Id || p.Id ==Id).Any())
                            {
                                
                            }
                            else
                            {
                                result.Add(item2);
                            }
                        }
                    }
                    else if(QueryWithTage.Count() == 1)
                    {
                        if (result.Where(p => p.Id == QueryWithTage.FirstOrDefault().Id || p.Id == Id).Any())
                        {

                        }
                        else
                        {
                            result.Add(QueryWithTage.FirstOrDefault());
                        }
                    }
                 
                   
                }
              
                
            }
           
            if (result != null && result.Count() > 0)
            {

                var Products = _mapper.Map<List<ProductsFormUserList_Dto>>(result.OrderByDescending(p=>p.Id).Take(cont));
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
