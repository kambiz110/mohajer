using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Command.CreatProductService;
using Mohajer.Service.User.Command.RegisterUser;
using Mohajer.Service.User.Command.UpdateUser;
using Mohajer.Service.User.Dto;
using Mohajer.Service.User.Query;
using Mohajer.Useful.Pager;
using Mohajer.Useful.Result;
using Mohajer.Useful.Static;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UsersController : Controller
    {
        private readonly IMapper _mapper;
        private readonly MohajerContext _mohajerContext;
        private readonly IRegisterUserService repoIRegisterUserService;
        private readonly IGetUseresForAdminService _getUseres;
        private readonly IUpdateUserService repoupdateUserService;
        public UsersController(IMapper mapper, MohajerContext mohajerContext, IRegisterUserService repoIRegisterUserService, IGetUseresForAdminService getUseres, IUpdateUserService repoupdateUserService)
        {
            this._mapper = mapper;
            this._mohajerContext = mohajerContext;
            this.repoIRegisterUserService = repoIRegisterUserService;
            this._getUseres = getUseres;
            this.repoupdateUserService = repoupdateUserService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// افزودن کاربر پنل مدیر
        /// </summary>
        /// <returns></returns>
        public IActionResult Register()
        {
            StaticList statik = new StaticList();
            ViewData["Role"] = statik.listeRoles;
            return View();
        }
        [HttpPost]
        public IActionResult Register(RegisterDto dto, IFormFile Image)
        {
            if (ModelState.IsValid)
            {
                var result = repoIRegisterUserService.Exequte(dto, Image);
                if (result.IsSuccess)
                {
                    return RedirectToAction("ManegeUser");
                }
            }
            StaticList statik = new StaticList();
            ViewData["Role"] = statik.listeRoles;
            return View();
        }
        public IActionResult ManegeUser(int PageSize = 5, int PageNo = 1)
        {
            var result = _getUseres.Exequte(PageSize, PageNo);
            if (result.Data != null)
            {
                UserForAdminServiceListDto model = new UserForAdminServiceListDto();
                model = result.Data;
                return View(model);
            }
            return View(new List<UserShowListViewModel>());
        }
        [HttpGet]
        public IActionResult EditeUser(long Id)
        {
            StaticList statik = new StaticList();
            ViewData["Role"] = statik.listeRolesEdited2;
            var user = _mohajerContext.Users.Where(p => p.Id == Id).FirstOrDefault();
            var model = _mapper.Map<RegisterUserViewModel>(user);
            return View(model);

        }
        [HttpPost]
        public IActionResult EditeUser(RegisterUserViewModel dto, IFormFile Image)
        {
            StaticList statik = new StaticList();
            ViewData["Role"] = statik.listeRolesEdited;
            var user = _mohajerContext.Users.Where(p => p.Id == dto.Id).AsNoTracking().FirstOrDefault();
            if (user != null)
            {
                var result = repoupdateUserService.UpdateUser(dto, Image, dto.Id);
                if (result.IsSuccess)
                {
                    return RedirectToAction("ManegeUser");
                }
                else
                {
                    return View(dto);
                }
            }

            else
            {
                return View(dto);
            }
        }
        [HttpGet]
        public IActionResult DeleteUser(long Id)
        {
            var user = _mohajerContext.Users.Where(p => p.Id == Id).FirstOrDefault();
            if (user != null)
            {
                user.IsRemove = true;
                user.Removed = DateTime.Now;
                _mohajerContext.Update(user);
                var resualt = _mohajerContext.SaveChanges();
            }
            return RedirectToAction("ManegeUser");
        }
        [HttpGet]
        public IActionResult ChangeStatusUser(long Id)
        {
            var user = _mohajerContext.Users.Where(p => p.Id == Id).FirstOrDefault();
            if (user != null)
            {
                user.IsActive = !user.IsActive;
                _mohajerContext.Update(user);
                var resualt = _mohajerContext.SaveChanges();
            }
            return RedirectToAction("ManegeUser");
        }
    }
}
