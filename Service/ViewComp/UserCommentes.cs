using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class UserCommentesViewComponent : ViewComponent
    {
        private readonly MohajerContext _mohajerContext;

        public UserCommentesViewComponent(MohajerContext mohajerContext)
        {
            _mohajerContext = mohajerContext;
        }
        public async Task<IViewComponentResult> InvokeAsync(long id)
        {
            var model = await _mohajerContext.Comments
               .Where(p => p.IsActive == true && p.IsRemove == false && p.ProdoctId==id)
               .Include(p => p.Prodoct)
               .ToListAsync();
            return await Task.FromResult((IViewComponentResult)View("UserCommentes", model));
        }
    }
}
