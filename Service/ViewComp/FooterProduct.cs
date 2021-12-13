using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class FooterProductViewComponent : ViewComponent
    {
        private readonly IGetProductFooter _getProductFooter;

        public FooterProductViewComponent(IGetProductFooter getProductFooter)
        {
            _getProductFooter = getProductFooter;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = _getProductFooter.Exequte().Data;
            return await Task.FromResult((IViewComponentResult)View("FooterProduct", model));
        }
    }
}
