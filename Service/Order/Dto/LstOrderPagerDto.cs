using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Order.Dto
{
    public class LstOrderPagerDto
    {
        public PagerDto PagerDto { get; set; }
        public List<OrderViewModel> LSTOrderDtos { get; set; }
    }
}
