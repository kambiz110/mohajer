using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Category.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class SideBarBlogViewComponent : ViewComponent
    {

        private readonly IGetCategorySideBare _getCategorySideBare;

        public SideBarBlogViewComponent(IGetCategorySideBare getCategorySideBare)
        {
            _getCategorySideBare = getCategorySideBare;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _getCategorySideBare.Exequt().Data;
            return await Task.FromResult((IViewComponentResult)View("SideBarBlog", model));
        }
    }
}
