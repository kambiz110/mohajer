using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class UserShowListViewModel
    {
        public long Id { get; set; }

        [Display(Name = " نام ")]
        public string First_Name { get; set; }

        [Display(Name = "نام خانوادگی ")]
        public string Last_Name { get; set; }

        [Display(Name = "ایمیل ")]
        public string Email { get; set; }

        [Display(Name = "شماره تلفن همراه ")]
        public string Phone { get; set; }

        [Display(Name = "نقش کاربری ")]
        public string Role { get; set; }

        [Display(Name = "عکس ")]
        public string Image { get; set; }

        [Display(Name = "وضعیت")]
        public bool IsActive { get; set; }
    }
}
