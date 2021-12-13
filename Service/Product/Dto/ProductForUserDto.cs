using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Dto
{
    public class ProductForUserDto
    {
        public PagerDto PagerDto { get; set; }

        public List<ProductsFormUserList_Dto> Products { get; set; }
    }
}
