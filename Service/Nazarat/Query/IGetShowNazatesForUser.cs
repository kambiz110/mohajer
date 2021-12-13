using Mohajer.Entity;
using Mohajer.Service.Nazarat.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Nazarat.Query
{
    public interface IGetShowNazatesForUser
    {
        ResultDto<List<NazaratViewModel>> Excute(int PageSize);
    }
    public class GetShowNazatesForUser : IGetShowNazatesForUser
    {
        private readonly MohajerContext _mohajerContext;

        public GetShowNazatesForUser(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }

        public ResultDto<List<NazaratViewModel>> Excute(int PageSize)
        {

            var result = _mohajerContext.Nazarats.Where(p => p.IsActive == true && p.IsRemove == false).Take(PageSize).Select(p => new NazaratViewModel
            {
               
                Comment = p.Comment,
                Name = p.Name,
                Email = p.Email
            }).ToList();

            ListNazaratPagerDto listNazaratPagerDto = new ListNazaratPagerDto();

            if (result != null)
            {

                return new ResultDto<List<NazaratViewModel>>
                {
                    Data = result,
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام گردید"
                };
            }
            else
            {

                return new ResultDto<List<NazaratViewModel>>
                {

                    Data = null ,
                    IsSuccess = false,
                    Message = "موردی یافت نگردید"
                };
            }

        }
    }
}
