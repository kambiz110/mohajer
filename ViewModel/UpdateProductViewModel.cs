using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class UpdateProductViewModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Title { get; set; }

        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Description { get; set; }

        [Display(Name = " عکس عنوان ")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Image { get; set; }

        [Display(Name = " فایل قابل پخش")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Media { get; set; }

        [Display(Name = "نوع فایل")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string CountentType { get; set; }

        [Display(Name = "محل نمایش")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public bool SliderShow { get; set; } = false;
    }
}
