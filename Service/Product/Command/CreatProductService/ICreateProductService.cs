using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Mohajer.Entity;
using Mohajer.Useful.Result;
using Mohajer.Useful.UploadFile;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mohajer.Useful.Static;

namespace Mohajer.Service.Admin.Product.Command.CreatProductService
{
    /// <summary>
    /// عملیات افزودن محصول جدید
    /// </summary>
    public interface ICreateProductService
    {
        ResultDto Exequte(ProductViewModel model, IFormFile Image, IFormFile Media ,long userId);
    }
    /// <summary>
    /// عملیات افزودن محصول جدید
    /// </summary>
    public class CreateProductService : ICreateProductService
    {
        private readonly MohajerContext mohajerContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public CreateProductService(MohajerContext mohajerContext, IWebHostEnvironment hostingEnvironment)
        {
            this.mohajerContext = mohajerContext;
            _hostingEnvironment = hostingEnvironment;
        }

        public ResultDto Exequte(ProductViewModel model, IFormFile Image, IFormFile Media, long userId)
        {
            int resaalt = 0;
            UploadFile uploadFile = new UploadFile(_hostingEnvironment);
            Prodoct prodoct = new Prodoct();

            if (Image != null)
            {
                var resultUpImg = uploadFile.UploadImage(Image);

                int contentType= Int32.Parse(model.CountentType);
                prodoct.CountentType = contentType;
                prodoct.Image = resultUpImg.FileNameAddress;
                prodoct.ImageResized = resultUpImg.FileNameAddressResize;
                prodoct.IsActive = false;
                prodoct.SliderShow = model.SliderShow;
                prodoct.Title = model.Title;
                prodoct.Description = model.Description;
                prodoct.Inserted = DateTime.Now;
                prodoct.Updated = DateTime.Now;
                prodoct.UserId = userId;
                prodoct.IsRemove = false;
               
                prodoct.CategoryId = model.CategoryId;
                prodoct.Link = model.Link;


                if (Media != null)
                {
                    
                    if (System.IO.File.Exists(prodoct.Media))
                    {
                        System.IO.File.Delete(prodoct.Media);
                    }
                    var resultUpFile = uploadFile.UploadMedia(Media);
                    prodoct.Media = resultUpFile.FileNameAddress;

                }
                var arr = model.Tages.ToArryTag();
                foreach (var item in arr)
                {
                    var result = mohajerContext.Tages.Where(p => p.Title == item.ToString().Trim()).FirstOrDefault();
                    if (result==null)
                    {
                        mohajerContext.Tages.Add(new Tage { Title = item.ToString().Trim() });
                      var test=  mohajerContext.SaveChanges();
                    }
                }
                prodoct.Tages = arr.ArryTagToString();
                mohajerContext.Prodoctes.Add(prodoct);
                resaalt = mohajerContext.SaveChanges();

            }
            if (resaalt > 0)
            {
                return new ResultDto()
                {

                    IsSuccess = true,
                    Message = "عملیات درج با موفقیت صورت پذیرفت"
                };
            }
            else
            {
                return new ResultDto()
                {

                    IsSuccess = false,
                    Message = "عملیات درج نامو فق بود"
                };
            }
        }
    }
}
