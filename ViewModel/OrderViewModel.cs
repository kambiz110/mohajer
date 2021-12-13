using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class OrderViewModel
    {
        public long Id { get; set; }
        [Display(Name = "نام ")]
        public string Full_Name { get; set; }
        [Display(Name = "عنوان")]
        public string Subject { get; set; }
        [Display(Name = "توضیحات")]
        public string Description { get; set; }
        [Display(Name = "تلفن")]
        public string Phone { get; set; }
        [Display(Name = "تاریخ")]
        [DataType(DataType.Date)]
        public DateTime AtInserted { get; set; }
        [Display(Name = "IP")]
        public string Ip { get; set; }
    }
}
