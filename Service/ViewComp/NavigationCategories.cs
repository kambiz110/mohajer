using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class NavigationCategoriesViewComponent : ViewComponent
    {
        private readonly MohajerContext _mohajerContext;

        public NavigationCategoriesViewComponent(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mohajerContext.Categores
                .Where(p=>p.ParentCategoryId==null)
               .ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("NavigationCategories", model));
        }
    }
}
