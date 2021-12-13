using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Mohajer.Entity;
using Mohajer.Service.User.Dto;
using Mohajer.Useful.Result;
using Mohajer.Useful.UploadFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.User.Command.RegisterUser
{
    public interface IRegisterUserService
    {
        ResultDto Exequte(RegisterDto dto, IFormFile Image);
    }
    public class RegisterUserService : IRegisterUserService
    {
        private readonly MohajerContext mohajerContext;
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IMapper _mapper;
        public RegisterUserService(IWebHostEnvironment hostingEnvironment, MohajerContext mohajerContext, IMapper mapper)
        {
            _hostingEnvironment = hostingEnvironment;
            this.mohajerContext = mohajerContext;
            _mapper = mapper;
        }

        public ResultDto Exequte(RegisterDto dto, IFormFile Image)
        {
            
            UploadFile uploadFile = new UploadFile(_hostingEnvironment);
            Entity.User u = new Entity.User();

            if (Image != null)
            {
                var resultUpImg = uploadFile.UploadProfile(Image);
                if (resultUpImg.Status)
                {
                    u = _mapper.Map<Entity.User>(dto);
                    u.Inserted = DateTime.Now;
                    u.Updated = DateTime.Now;
                    u.IsActive = true;
                    u.Image = resultUpImg.FileNameAddress;
                    mohajerContext.Users.Add(u);
                     mohajerContext.SaveChanges();
                    return new ResultDto
                    {
                        IsSuccess = true,
                        Message = " عملیات درج کاربر  موفق بود"
                    };
                }
                return new ResultDto
                {
                    IsSuccess = true,
                    Message = " عملیات درج کاربر نا موفق بود"
                };
            }
            else
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Message = " عملیات درج کاربر بعلت عدم وجود عکس نا موفق بود"
                };
            }

        }
    }
}
