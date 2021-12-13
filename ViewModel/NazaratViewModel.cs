using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class NazaratViewModel
    {
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Email { get; set; }

        [Display(Name = "نام کاربری")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Name { get; set; }

        [Display(Name = "پیغام")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Comment { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "تاریخ درج نظر")]
        public DateTime AtInserted { get; set; }


    }
}
