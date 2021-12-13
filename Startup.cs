using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Microsoft.AspNetCore.Http.Features;
using AutoMapper;
using Mohajer.Useful.AutoMapper;
using System.Net;
using Mohajer.Service.Admin.Product.Command.CreatProductService;
using Microsoft.AspNetCore.Authentication.Cookies;
using Mohajer.Service.User.Command.RegisterUser;
using Mohajer.Service.Admin.Product.Query;
using Mohajer.Service.User.Query;
using Mohajer.Service.Product.Command.UpdateProduct;
using Mohajer.Service.User.Command.UpdateUser;
using DNTCaptcha.Core;
using Microsoft.AspNetCore.HttpOverrides;
using Mohajer.Service.Nazarat.Command.AddNazar;
using Mohajer.Service.Nazarat.Query;
using Mohajer.Service.Product.Query;
using Mohajer.Service.Category.Query;
using Mohajer.Service.Comment.Command;
using Mohajer.Service.Comment.Query;
using AspNetCoreRateLimit;
using Mohajer.Service.ViewComp;
using Mohajer.Service.Order.Command.AddOrder;
using Mohajer.Service.Order.Query;

namespace Mohajer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MohajerContext>(options => options.
             UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(60);
            });
            services.AddControllersWithViews();
            services.AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            }).AddCookie(options =>
            {
                options.LoginPath = new PathString("/");
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5.0);
            });

            //services.Configure<FormOptions>(x =>
            //{
            //    x.MultipartBodyLengthLimit = 60000000000;
            //});
            services.AddDNTCaptcha(options =>
            options.UseCookieStorageProvider()
           .ShowThousandsSeparators(false)
            );


            // Auto Mapper Configurations
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MappingProfile());
            });

            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddScoped<ICreateProductService, CreateProductService>();
            services.AddScoped<IRegisterUserService, RegisterUserService>();
            services.AddScoped<IGetProductesForAdminService, GetProductesForAdminService>();
            services.AddScoped<IGetUseresForAdminService, GetUseresForAdminService>();
            services.AddScoped<IUpdateProductService, UpdateProductService>();
            services.AddScoped<IUpdateUserService, UpdateUserService>();

            services.AddScoped<IAddNazar, AddNazar>();
            services.AddScoped<IGetNazarat, GetNazarat>();
            services.AddScoped<IGetProductForEndPointUser, GetProductForEndPointUser>();
            services.AddScoped<IGetProductForShowBlogDeteils, GetProductForShowBlogDeteils>();
            services.AddScoped<IGetCategorySideBare, GetCategorySideBare>();
            services.AddScoped<IGetProductesWithCategoryId, GetProductesWithCategoryId>();
            services.AddScoped<IGetProductWithTag, GetProductWithTag>();
            services.AddScoped<IGetProductWithMediaType, GetProductWithMediaType>();
            services.AddScoped<IGetProductRelatedPosts, GetProductRelatedPosts>();
            services.AddScoped<IAddComment, AddComment>();
            services.AddScoped<IGetCommentAdmin, GetCommentAdmin>();
            services.AddScoped<IGetProductFooter, GetProductFooter>();
            services.AddScoped<IGetProductForMainPageIndex, GetProductForMainPageIndex>();
            services.AddScoped<IAddOrder, AddOrder> ();
            services.AddScoped<IGetOrder, GetOrder>();
            

            initRateLimit(services);


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });
            app.UseIpRateLimiting();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                ForwardedHeaders.XForwardedProto
            });
            //Addd User session - JRozario
            app.UseSession();
            app.UseStatusCodePages(Ppp);
            //Add JWToken Authentication service - JRozario
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "AdminArea",
                     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        private static Task Ppp(Microsoft.AspNetCore.Diagnostics.StatusCodeContext context)
        {
            var request = context.HttpContext.Request;


            var response = context.HttpContext.Response;

            if (response.StatusCode == (int)HttpStatusCode.Unauthorized)
            // you may also check requests path to do this only for specific methods       
            // && request.Path.Value.StartsWith("/specificPath")

            {
                response.Redirect("/Home/Index/");
            }
            return null;
        }

        private void initRateLimit(IServiceCollection services)
        {
            // needed to load configuration from appsettings.json
            services.AddOptions();

            // needed to store rate limit counters and ip rules
            services.AddMemoryCache();

            //load general configuration from appsettings.json
            services.Configure<IpRateLimitOptions>(Configuration.GetSection("IpRateLimiting"));

            //load ip rules from appsettings.json
            services.Configure<IpRateLimitPolicies>(Configuration.GetSection("IpRateLimitPolicies"));

            // inject counter and rules stores
            services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
            services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();

            // Add framework services.
            services.AddMvc();

            // configuration (resolvers, counter key builders)
            services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        }
    }
}
