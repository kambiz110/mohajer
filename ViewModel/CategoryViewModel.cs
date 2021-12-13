using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        [Display(Name ="عنوان")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Title { get; set; }
        [Display(Name = "عنوان والد")]
        public int? ParentCategoryId { get; set; }
        [Display(Name = "عنوان والد")]
        public Category ParentCategory { get; set; }
    }
}
