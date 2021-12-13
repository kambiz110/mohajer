using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{

    public class SliderViewComponent : ViewComponent
    {
        private readonly MohajerContext _mohajerContext;

        public SliderViewComponent( MohajerContext mohajerContext)
        {
             _mohajerContext = mohajerContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mohajerContext.Prodoctes
                .Where(p =>p.SliderShow ==true && p.IsActive==true && p.IsRemove==false)
                .OrderByDescending(p => p.Id)
                .Take(6)
                .ToListAsync();

            return View(model);
        }
    }
}
