using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Service.User.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.User.Query
{
    public interface IGetUseresForAdminService
    {
        ResultDto<UserForAdminServiceListDto> Exequte(int PageSize, int PageNo);
    }


    public class GetUseresForAdminService : IGetUseresForAdminService
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IMapper _mapper;
        public GetUseresForAdminService(MohajerContext mohajerContext, IMapper mapper)
        {
            _mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto<UserForAdminServiceListDto> Exequte(int PageSize, int PageNo)
        {
            int rowCount = 0;

            var result = _mohajerContext.Users.Where(p => p.IsRemove == false).ToPaged(PageNo, PageSize, out rowCount).ToList();
            if (result != null && result.Count() > 0)
            {
                var model = new UserForAdminServiceListDto
                {
                    userShows = _mapper.Map<List<UserShowListViewModel>>(result),
                    PagerDto = new PagerDto
                    {
                        PageNo = PageNo,
                        PageSize = PageSize,
                        TotalRecords = rowCount
                    }
                };
                return new ResultDto<UserForAdminServiceListDto>
                {

                    Data = model,
                    IsSuccess = true,
                    Message = "عملیات موفق بود"

                };
            }
            return new ResultDto<UserForAdminServiceListDto>
            {

                Data = null,
                IsSuccess = false,
                Message = "عملیات موفق بود"

            };


        }
    }
}
