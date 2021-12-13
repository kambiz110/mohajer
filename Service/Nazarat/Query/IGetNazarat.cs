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
    public interface IGetNazarat
    {
        ResultDto<ListNazaratPagerDto> Excute(int PageSize, int PageNo ,bool isConfim);
    }
    public class GetNazarat : IGetNazarat
    {
        private readonly MohajerContext _mohajerContext;

        public GetNazarat(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }

        public ResultDto<ListNazaratPagerDto> Excute(int PageSize, int PageNo, bool isConfim=false)
        {
            int rowCount = 0;
            var result = _mohajerContext.Nazarats.Where(p => p.IsActive == isConfim && p.IsRemove==false).ToPaged(PageNo, PageSize, out rowCount).Select(p => new NazaratViewDto
            {
                Id = p.Id,
                Comment = p.Comment,
                Name = p.Name,
                Email = p.Email
            }).ToList();
            PagerDto pag = new PagerDto
            {
                PageNo = PageNo,
                PageSize = PageSize,
                TotalRecords = rowCount 
            };
            ListNazaratPagerDto listNazaratPagerDto = new ListNazaratPagerDto();
            
            if (result != null)
            {
                listNazaratPagerDto.nazarShows = result;
                listNazaratPagerDto.PagerDto = pag;
                return new ResultDto<ListNazaratPagerDto>
                {
                    Data = listNazaratPagerDto,
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام گردید"
                };
            }
            else
            {
                listNazaratPagerDto.nazarShows = null;
                listNazaratPagerDto.PagerDto = null;
                return new ResultDto<ListNazaratPagerDto>
                {

                    Data = listNazaratPagerDto,
                    IsSuccess = false,
                    Message = "موردی یافت نگردید"
                };
            }
     

        }
    }
}
