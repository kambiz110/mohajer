using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.ViewModel
{
    public class ProductList_Pager_ViewModel
    {
        public List<ProductListViewModel> productList { get; set; } 
        public PagerDto pager { get; set; }
    }
}
