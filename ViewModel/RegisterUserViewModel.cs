using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class RegisterUserViewModel
    {
        public long Id { get; set; }

        [Display(Name = "نام")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string First_Name { get; set; }

        [Display(Name = "نام خانوادگی")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Last_Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "فرمت ورود ایمیل نادرست می باشد")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن همراه")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "فرمت ورود شماره موبایل نادرست می باشد")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Phone { get; set; }

        [Display(Name = "رمز عبور")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        [Compare(nameof(Password) ,ErrorMessage ="رمز وارد شده با رمز بالا مغایرت دارد")]
        public string RePassword { get; set; }

        [Display(Name = "نقش")]
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Role { get; set; }

        [Display(Name = "عکس")]
       
        [Required(ErrorMessage = "{0} یک فیلد اجباری است و باید آن را وارد کنید.")]
        public string Image { get; set; }

        [Display(Name = "گروه کاری")]
        public bool IsWorkUser { get; set; }
    }
}
