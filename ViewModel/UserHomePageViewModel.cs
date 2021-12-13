using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class UserHomePageViewModel
    {
        [Display(Name = " نام ")]
    
        public string First_Name { get; set; }
        [Display(Name = "نام خانوادگی ")]

        public string Last_Name { get; set; }
        [Display(Name = "ایمیل ")]
 
        public string Email { get; set; }
        [Display(Name = "عکس ")]

        public string Image { get; set; }

        [Display(Name = "نقش کاربری ")]

        public string Role { get; set; }
    }
}
