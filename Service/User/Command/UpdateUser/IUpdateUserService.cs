using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Service.User.Dto;
using Mohajer.Useful.Result;
using Mohajer.Useful.UploadFile;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.User.Command.UpdateUser
{
    public interface IUpdateUserService
    {
        ResultDto UpdateUser(RegisterUserViewModel dto, IFormFile Image , long Id);
    }
    public class UpdateUserService : IUpdateUserService
    {
        private readonly MohajerContext mohajerContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;

        public UpdateUserService(MohajerContext mohajerContext, IWebHostEnvironment hostingEnvironment, IMapper mapper)
        {
            this.mohajerContext = mohajerContext;
            _hostingEnvironment = hostingEnvironment;
            _mapper = mapper;
        }

        public ResultDto UpdateUser(RegisterUserViewModel dto, IFormFile Image, long Id)
        {
            var oldUser = mohajerContext.Users.Where(p => p.Id == dto.Id).AsNoTracking().FirstOrDefault();
            var Newuser = new Mohajer.Entity.User
            {
                Id = dto.Id,
                Email = dto.Email,
                First_Name= dto.First_Name,
                Last_Name=dto.Last_Name,
                Password=dto.Password,
                Phone=dto.Phone,
                Role=dto.Role,
                IsWorkUser=dto.IsWorkUser,
                Image= oldUser.Image


            };
           
            if (Image !=null)
            {
                UploadFile uploadFile = new UploadFile(_hostingEnvironment);
               
                var resultUpImg = uploadFile.UploadImage(Image);
                Newuser.Image = resultUpImg.FileNameAddress;
            }
            Newuser.Updated = DateTime.Now;
            Newuser.IsActive = true;
           
            mohajerContext.Update(Newuser);
            var resualt = mohajerContext.SaveChanges();
            if (resualt > 0)
            {
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = "عملیات بروز رسانی موفق بود"
                };

            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = "عملیات بروز رسانی نا موفق بود"
                };
            }
        }
    }
}
