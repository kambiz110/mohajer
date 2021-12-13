using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class TeamViewComponent : ViewComponent
    {

        private readonly MohajerContext _mohajerContext;

        public TeamViewComponent(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _mohajerContext.Users
               .Where(p => p.IsActive == true && p.IsWorkUser == true && p.IsRemove==false)
               .OrderByDescending(p => p.Id)
               .Take(20)
               .Select(p => new UserHomePageViewModel
               {
                   First_Name = p.First_Name,
                   Last_Name = p.Last_Name,
                   Email = p.Email,
                   Image = p.Image,
                   Role = p.Role
               })
               .ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("Team", model));
        }

    }
}
