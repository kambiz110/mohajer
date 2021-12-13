using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }

        [Display(Name ="تاریخ ایجاد")]
        public DateTime Inserted { get; set; }

        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime Updated { get; set; }

        [Display(Name = "تاریخ حذف")]
        public DateTime? Removed { get; set; }

        [Display(Name = "حذف شده")]
        public bool? IsRemove { get; set; } = false;

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
