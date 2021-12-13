using Microsoft.AspNetCore.Mvc;
using Mohajer.Service.Product.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.ViewComp
{
    public class RelatedPostesViewComponent : ViewComponent
    {
        private readonly IGetProductRelatedPosts _getProductRelatedPosts;

        public RelatedPostesViewComponent(IGetProductRelatedPosts getProductRelatedPosts)
        {
            _getProductRelatedPosts = getProductRelatedPosts;
        }

        public async Task<IViewComponentResult> InvokeAsync(long Id, int cont=2)
        {
            var model = _getProductRelatedPosts.Exequte(Id , cont).Data;
            return await Task.FromResult((IViewComponentResult)View("RelatedPostes", model));
        }
    }
}
