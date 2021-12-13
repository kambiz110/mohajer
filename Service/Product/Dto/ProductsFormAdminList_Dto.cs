using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Admin.Product.Dto
{
    public class ProductsFormAdminList_Dto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageResized { get; set; }
        public string Media { get; set; }
        public int CountentType { get; set; }
        public bool SliderShow { get; set; }
        public bool IsActive { get; set; }

    }
}
