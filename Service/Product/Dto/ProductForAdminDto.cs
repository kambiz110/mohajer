using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Admin.Product.Dto
{
    public class ProductForAdminDto
    {
        public PagerDto PagerDto { get; set; }

        public List<ProductsFormAdminList_Dto> Products { get; set; }
    }
}
