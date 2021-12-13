using Mohajer.Service.Nazarat.Dto;
using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Comment.Dto
{
    public class ListCommentPagerDto
    {
        public PagerDto PagerDto { get; set; }
        public List<CommentDto> LSTcommentDtos { get; set; }
    }
}
