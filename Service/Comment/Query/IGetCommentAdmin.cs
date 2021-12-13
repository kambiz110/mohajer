using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Comment.Dto;
using Mohajer.Service.Nazarat.Dto;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Comment.Query
{
    public interface IGetCommentAdmin
    {
        ResultDto<ListCommentPagerDto> Excute(int PageSize, int PageNo, bool isConfim);
    }
    public class GetCommentAdmin : IGetCommentAdmin
    {
        private readonly MohajerContext _mohajerContext;

        public GetCommentAdmin(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }

        public ResultDto<ListCommentPagerDto> Excute(int PageSize, int PageNo, bool isConfim=false)
        {
            int rowCount = 0;
            var result = _mohajerContext.Comments.Where(p => p.IsActive == isConfim && p.IsRemove == false)
                .Include(p => p.Prodoct)
                .ToPaged(PageNo, PageSize, out rowCount).Select(p => new CommentDto
            {
                Id = p.Id,
                Ip = p.Ip ,
                AtInserted = p.AtInserted,
                CommentMsg= p.CommentMsg,
                Name = p.Name,
                Email = p.Email,
                ProductTitle= p.Prodoct.Title,
                ProductId=p.Prodoct.Id,
                AnserMsg= p.AnserMsg
            }).ToList();
            PagerDto pag = new PagerDto
            {
                PageNo = PageNo,
                PageSize = PageSize,
                TotalRecords = rowCount
            };
            ListCommentPagerDto listCommentPagerDto = new ListCommentPagerDto();

            if (result != null)
            {
                listCommentPagerDto.LSTcommentDtos = result;
                listCommentPagerDto.PagerDto = pag;
                return new ResultDto<ListCommentPagerDto>
                {
                    Data = listCommentPagerDto,
                    IsSuccess = true,
                    Message = "عملیات با موفقیت انجام گردید"
                };
            }
            else
            {
                listCommentPagerDto.LSTcommentDtos = null;
                listCommentPagerDto.PagerDto = null;
                return new ResultDto<ListCommentPagerDto>
                {

                    Data = listCommentPagerDto,
                    IsSuccess = false,
                    Message = "موردی یافت نگردید"
                };
            }
        }
    }
}
