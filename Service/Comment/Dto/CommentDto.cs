using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Nazarat.Dto
{
    public class CommentDto
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        [Display(Name = "نام")]
        public string Name { get; set; }
        [Display(Name = "ایمیل")]
        public string Email { get; set; }
        [Display(Name = "عنوان محتوا")]
        public string ProductTitle { get; set; }
        [Display(Name = "پیام کاربر")]
        public string CommentMsg { get; set; }
        [Display(Name = "IP")]
        public string Ip { get; set; }
        [Display(Name = "عنوان")]
        [DataType(DataType.Date)]
        public DateTime AtInserted { get; set; }
        [Display(Name = "پاسخ")]
        public string AnserMsg { get; set; }
    }
}
