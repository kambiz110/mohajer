using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Product.Dto;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Query
{/// <summary>
///واکشی اطلاعات محتوا برای نمایش در صفحه اصلی
/// count
/// </summary>
    public interface IGetProductForMainPageIndex
    {
        ResultDto<List<ProductForHomePageDto>> Exqute(int count);
    }
    public class GetProductForMainPageIndex : IGetProductForMainPageIndex
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;

        public GetProductForMainPageIndex(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        private List<ProductCatergoryMedia> negetive = new List<ProductCatergoryMedia>();
        private List<ProductCatergoryMedia> positive = new List<ProductCatergoryMedia>();
        private List<ProductForHomePageDto> LstResultDto = new List<ProductForHomePageDto>();
        public ResultDto<List<ProductForHomePageDto>> Exqute(int count)
        {
            int counter = (int)count / 3;
            int countImg = (int)count / 3;
            int countVideo = (int)count / 3;
            int countMusic = (int)count/3;
            List<int> arryType = new List<int>() { 0, 1, 2 };


            CheckPro(0, counter);
            CheckPro(1, counter);
            CheckPro(2, counter);

            if (positive.Count() == 3 || negetive.Count() == 3)
            {
                AddToListProductForHomePageDto( arryType[0], counter , arryType[1], counter + 1 , arryType[2], counter );
                return new ResultDto<List<ProductForHomePageDto>> { 
                Data= LstResultDto ,
                IsSuccess=true , 
                Message="با مو فقیت انجام شد."
                };
            }


            if (negetive.Count()==1 && positive.Count()==2)
            {
                var typed = negetive.FirstOrDefault().ctegoryType;
                arryType.Remove(typed);
                AddToListProductForHomePageDto(typed , counter , arryType[0] , counter+ ((int)counter / 2), arryType[1], counter + ((int)counter / 2));

                return new ResultDto<List<ProductForHomePageDto>>
                {
                    Data = LstResultDto,
                    IsSuccess = true,
                    Message = "با مو فقیت انجام شد."
                };
            }

            if (negetive.Count() == 2 && positive.Count() == 1)
            {
                var typed = negetive.ElementAt(0).ctegoryType;
                var typed2 = negetive.ElementAt(1).ctegoryType;
                arryType.Remove(typed);
                arryType.Remove(typed2);
                AddToListProductForHomePageDto(typed, counter, typed2, counter ,arryType[0] , counter*3+1);

                return new ResultDto<List<ProductForHomePageDto>>
                {
                    Data = LstResultDto,
                    IsSuccess = true,
                    Message = "با مو فقیت انجام شد."
                };
            }

            return new ResultDto<List<ProductForHomePageDto>>
            {
                Data = null,
                IsSuccess = false,
                Message = "نا مو فق بود."
            };
        }


        private void CheckPro(int typeCheckPro, int countCheckPro) {
            var result = _mohajerContext.Prodoctes
           .Where(p => p.IsActive == true && p.IsRemove == false && p.CountentType == typeCheckPro)
           .Select(o => new ProductCatergoryMedia
           {
               ctegoryType = typeCheckPro,
               countObj = _mohajerContext.Prodoctes
           .Where(p => p.IsActive == true && p.IsRemove == false && p.CountentType == typeCheckPro).Count() - countCheckPro
           }
           ).FirstOrDefault();
            AddToList(result , typeCheckPro, countCheckPro);
        }
        private void AddToList(ProductCatergoryMedia _result , int type , int count)
        {
            if (_result!=null && _result.countObj > 0)
            {
                positive.Add(_result);
            }
            else if(_result == null)
            {
                negetive.Add(new ProductCatergoryMedia { 
                ctegoryType= type ,
                countObj= 0 - count
                });
            }
            else
            {
                negetive.Add(_result);
            }

        }
        private void AddToListProductForHomePageDto(int type1, int counttype1, int type2, int counttype2, int type3 , int counttype3)
        {
            AddToListProductForHomePageDto(type1 , counttype1);
            AddToListProductForHomePageDto(type2, counttype2);
            AddToListProductForHomePageDto(type3, counttype3);

        }
        private void AddToListProductForHomePageDto(int type, int counttype)
        {
            var result = _mohajerContext.Prodoctes.Where(p => p.IsActive == true && p.IsRemove == false && p.CategoryId == type)
                            .Include(p => p.User)
                            .ToList();
            var model = _mapper.Map<List<ProductForHomePageDto>>(result);
            LstResultDto.AddRange(model);
        }

    }
    public class ProductCatergoryMedia
    {
        public int ctegoryType { get; set; }
        public int countObj { get; set; }
    }
}
