using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Command.CreatProductService;
using Mohajer.Service.Admin.Product.Query;
using Mohajer.Service.Product.Command.UpdateProduct;
using Mohajer.Service.User.Command.RegisterUser;
using Mohajer.Useful.Static;
using Mohajer.Useful.UploadFile;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MohajerContext _mohajerContext;
        private readonly IGetProductesForAdminService _getProductes;
        private readonly ICreateProductService repoICreateProductService;
        private readonly IUpdateProductService repoupdateProductService;
        public ProductController(IMapper mapper, MohajerContext mohajerContext, ICreateProductService repoICreateProductService, IGetProductesForAdminService getProductes, IUpdateProductService repoupdateProductService)
        {
            this._mapper = mapper;
            this._mohajerContext = mohajerContext;

            this.repoICreateProductService = repoICreateProductService;
            this._getProductes = getProductes;
            this.repoupdateProductService = repoupdateProductService;
        }

        /// <summary>
        /// نمایش محصولات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Poduct(int PageSize = 10, int PageNo = 1)
        {

            var result = _getProductes.Exequte(PageSize, PageNo ,false);
            ProductList_Pager_ViewModel model = new ProductList_Pager_ViewModel();
            if (result.Data != null)
            {
                
                model.productList = _mapper.Map<List<ProductListViewModel>>(result.Data.Products);
                model.pager = result.Data.PagerDto;
                return View(model);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult PoductConfimated(int PageSize = 10, int PageNo = 1 )
        {

            var result = _getProductes.Exequte(PageSize, PageNo, true);
            ProductList_Pager_ViewModel model = new ProductList_Pager_ViewModel();
            if (result.Data != null)
            {

                model.productList = _mapper.Map<List<ProductListViewModel>>(result.Data.Products);
                model.pager = result.Data.PagerDto;
                return View(model);
            }

            return View(model);
        }
        /// <summary>
        /// افزودن محصول جدید
        /// </summary>
        [HttpGet]
        public IActionResult CreatePoduct()
        {
            StaticList statik = new StaticList();
            ViewData["PlaceShow"] = statik.PlaceShow;
            ViewData["FileType"] = statik.FileType;
            ViewData["ParentCategoryId"] = new SelectList(_mohajerContext.Categores.Where(p => p.ParentCategoryId == null && p.IsRemove == false), "Id", "Title");
            return View();
        }
        /// <summary>
        /// افزودن محصول جدید
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Image"></param>
        /// <param name="Media"></param>
        /// <returns></returns>
        [AutoValidateAntiforgeryToken]
        [HttpPost]
        public IActionResult CreatePoduct(ProductViewModel model, IFormFile Image, IFormFile Media)
        {
            ModelState.Remove("Image");
            ModelState.Remove("Link");
            ModelState.Remove("Category");
            ModelState.Remove("Media");
            ModelState.Remove("Description");
            ModelState.Remove("ImageResized");
            StaticList statik = new StaticList();
            ViewData["PlaceShow"] = statik.PlaceShow;
            ViewData["FileType"] = statik.FileType;
            ViewData["ParentCategoryId"] = new SelectList(_mohajerContext.Categores.Where(p => p.ParentCategoryId == null && p.IsRemove == false), "Id", "Title");

            if (!ModelState.IsValid)
            {
                return View(model);
            }
            //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();
            // var UserIdClaime = claims?.FirstOrDefault(x => x.Type.Equals("USERID", StringComparison.OrdinalIgnoreCase))?.Value;

            long userId = _mohajerContext.Users.FirstOrDefault().Id;

            var result = repoICreateProductService.Exequte(model, Image, Media, userId);
            if (!result.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Poduct");
        }
        [HttpGet]
        public IActionResult DetailsProduct(long Id)
        {
            var product = _mohajerContext.Prodoctes.Where(p => p.Id == Id).FirstOrDefault();
            if (product != null && product.Id > 0)
            {
                StaticList statik = new StaticList();
                ViewData["PlaceShow"] = statik.PlaceShow;
                ViewData["FileType"] = statik.FileTypeCondition;
                
                ViewData["ParentCategoryId"] = new SelectList(_mohajerContext.Categores.Where(p => p.ParentCategoryId == null && p.IsRemove == false), "Id", "Title" , product.Category);
                ViewBag.Id = product.Id;
                ProductViewModel model = new ProductViewModel
                {
                    Title = product.Title,
                    Description = product.Description,
                    SliderShow = product.SliderShow,
                    CountentType = product.CountentType.ToString(),
                    Image = product.Image,
                    ImageResized= product.ImageResized,
                    Tages=product.Tages,
                    CategoryId=product.CategoryId ,
                    Category= product.Category,
                    Link=product.Link
                    
                };
                return View(model);
            }

            return RedirectToAction("Poduct");
        }
        [HttpPost]
        public IActionResult DetailsProduct(ProductViewModel model, IFormFile Image, IFormFile Media, long Id)
        {
            var product = _mohajerContext.Prodoctes.Where(p => p.Id == Id).FirstOrDefault();
            if (product != null && product.Id > 0)
            {
                var result = repoupdateProductService.UpdateProduct(model, Image, Media, product);
                if (result.IsSuccess)
                {
                    return RedirectToAction("Poduct");
                }

            }
            return View(model);
        }

        [HttpGet]
        public IActionResult DelateProduct(long Id)
        {
            var product = _mohajerContext.Prodoctes.Find(Id);
            product.IsRemove = true;
            product.Removed = DateTime.Now;
            _mohajerContext.Entry(product).State = EntityState.Modified;
            _mohajerContext.SaveChanges();
            return RedirectToAction("Poduct");
        }

        [HttpGet]
        public IActionResult ChangeStatusProduct(long Id)
        {
            var product = _mohajerContext.Prodoctes.Find(Id);
            product.IsActive = !product.IsActive;
            _mohajerContext.Update(product);
            _mohajerContext.SaveChanges();
            return RedirectToAction("Poduct");
        }
    }
}
