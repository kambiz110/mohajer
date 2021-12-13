using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class UserViewModel
    { 
        [Display(Name = " نام ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string First_Name { get; set; }
        [Display(Name = "نام خانوادگی ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string Last_Name { get; set; }
        [Display(Name = "ایمیل ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string Email { get; set; }
        [Display(Name = "شماره تلفن همراه ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string Phone { get; set; }
        [Display(Name = "نقش کاربری ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string Role { get; set; }


        [Display(Name = "رمز عبور ")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        public string Password { get; set; }

        [Display(Name = "تکرار رمز عبور")]
        [Required(ErrorMessage = "لطفا مقدار را وارد نمایئد ")]
        [Compare(nameof(Password) ,ErrorMessage ="پسورد وارد شده با مقدار قبلی یکسان نیست ")]
        public string RePassword { get; set; }
        [Display(Name = "عکس ")]

        public string Image { get; set; }

    }
}
