using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Category.Dto
{
    public class SideBareDto
    {
        public List<SideBarCategory> sideBarCategories { get; set; }
        public List<string> TagesSlidBare { get; set; }
        public List<ProductViewModel> products { get; set; }
    }
}
