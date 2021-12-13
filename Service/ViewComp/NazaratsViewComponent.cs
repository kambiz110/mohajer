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
    public class NazaratsViewComponent : ViewComponent
    {
    
        private readonly MohajerContext _mohajerContext;

        public NazaratsViewComponent( MohajerContext mohajerContext)
        {
         
            _mohajerContext = mohajerContext;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mohajerContext.Nazarats
               .Where(p => p.IsActive == true && p.IsRemove==false && p.Category==1)
               .OrderByDescending(p => p.Id)
               .Take(20)
               .ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("Nazarats" , model));
        }

    }
}
