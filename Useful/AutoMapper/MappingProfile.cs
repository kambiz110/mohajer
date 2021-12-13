using AutoMapper;
using Mohajer.Entity;
using Mohajer.Service.Admin.Product.Dto;
using Mohajer.Service.Admin.User.Dto;

using Mohajer.Service.Product.Dto;

using Mohajer.Service.User.Dto;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Useful.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add as many of these lines as you need to map your objects
            CreateMap<Prodoct, ProductForHomePageDto>();
            CreateMap<Prodoct, ProductListViewModel>();
            CreateMap<Prodoct, ProductsFormAdminList_Dto>();
            CreateMap<Prodoct, ProductsFormUserList_Dto>();
            CreateMap<Prodoct, ProductViewModel>();
            CreateMap<ProductsFormAdminList_Dto, ProductListViewModel>();
            CreateMap<RegisterDto, User>();
            CreateMap<User, UserShowListViewModel>();
            CreateMap<User, RegisterUserViewModel>();
            CreateMap<RegisterUserViewModel, User>();
            CreateMap<Category, CategoryViewModel>();
            CreateMap<User, LoginUserDto>();

            CreateMap<Order, OrderViewModel>();


            // CreateMap<UserDto, User>();   RegisterDto <RegisterUserViewModel>(user);
        }
    }
}
