using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Product.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class MeinContentViewComponent : ViewComponent
    {
        private readonly MohajerContext _mohajerContext;
        private readonly IGetProductForMainPageIndex _getProductForMainPageIndex;
        public MeinContentViewComponent(MohajerContext mohajerContext, IGetProductForMainPageIndex getProductForMainPageIndex)
        {
            _mohajerContext = mohajerContext;
            _getProductForMainPageIndex = getProductForMainPageIndex;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _getProductForMainPageIndex.Exqute(16).Data;
            return await Task.FromResult((IViewComponentResult)View("MeinContent", model));
        }
    }
}
