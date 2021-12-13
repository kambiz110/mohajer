using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Command.CreatProductService;
using Mohajer.Service.User.Command.RegisterUser;
using Mohajer.Service.User.Dto;
using Mohajer.Useful.Static;
using Mohajer.Useful.UploadFile;
using Mohajer.ViewModel;

namespace Mohajer.Areas.Admin.Controllers
{

    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MohajerContext _mohajerContext;   
        private readonly ICreateProductService repoICreateProductService;    
        public AdminController(IMapper mapper, MohajerContext mohajerContext, ICreateProductService repoICreateProductService)
        {
            this._mapper = mapper;
            this._mohajerContext = mohajerContext;
            this.repoICreateProductService = repoICreateProductService;
           
        }
        [HttpGet]
        public IActionResult Index()
        {
            var result = _mohajerContext.Prodoctes.Take(20).ToList();
            if (result !=null)
            {
                var model = _mapper.Map<List<ProductListViewModel>>(result);
                return View(model);
            }
           
            return View(new List<ProductListViewModel>());
        }
        /// <summary>
        /// نمایش محصولات
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Poduct()
        {
            //var exercises = _dbContext.Exercises.Where(x => x.Id == id).ToList();
            //return _mapper.Map<IEnumerable<ExerciseDto>>(exercises);
            return View();
        }
        /// <summary>
        /// افزودن محصول جدید
        /// </summary>
        public IActionResult CreatePoduct()
        {
            StaticList statik = new StaticList();
            ViewData["PlaceShow"] = statik.PlaceShow;
            ViewData["FileType"] = statik.FileType;
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
            ModelState.Remove("Media");
            ModelState.Remove("Description");
            StaticList statik = new StaticList();
            ViewData["PlaceShow"] = statik.PlaceShow;
            ViewData["FileType"] = statik.FileType;
            if (!ModelState.IsValid)
            {
                return View(model);
            }
          //var claims = ClaimsPrincipal.Current.Identities.FirstOrDefault().Claims.ToList();
           // var UserIdClaime = claims?.FirstOrDefault(x => x.Type.Equals("USERID", StringComparison.OrdinalIgnoreCase))?.Value;
       
           
            int userId = 1;
            var result = repoICreateProductService.Exequte(model , Image , Media , userId);
            if (!result.IsSuccess)
            {
                return View(model);
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdateProduct()
        {

            return View();
        }


        

    }
}

