using Microsoft.AspNetCore.Mvc.ModelBinding;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mohajer.ViewModel
{
    public class ProductViewModel
    {
        public long Id { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Title { get; set; }

        [DataType(DataType.MultilineText)]
        [Display(Name = "توضیحات")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Description { get; set; }
       
        [Display(Name = " عکس عنوان ")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Image { get; set; }
        public string ImageResized { get; set; }

        [Display(Name = " فایل قابل پخش")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Media { get; set; }

        [Display(Name = "نوع فایل")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string CountentType { get; set; }

        [Display(Name = "محل نمایش")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public bool SliderShow { get; set; } = false;

        [Display(Name = "کلمات کلیدی")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Tages { get; set; }


        [Display(Name = "لینک فایل")]
        public string Link { get; set; }


        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public long CategoryId { get; set; }
        [Display(Name = "دسته بندی")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public Category Category { get; set; }


        [Display(Name = "تاریخ بروزرسانی")]
        public DateTime Updated { get; set; }

    }
}
