using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Mohajer.Entity;
using Mohajer.Useful.Result;
using Mohajer.Useful.Static;
using Mohajer.Useful.UploadFile;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Command.UpdateProduct
{/// <summary>
/// بروزرسانی محتوا
/// </summary>
    public interface IUpdateProductService
    {
        ResultDto UpdateProduct(ProductViewModel model, IFormFile Image, IFormFile Media, Prodoct prodoct);
    }
    public class UpdateProductService : IUpdateProductService
    {
        private readonly MohajerContext mohajerContext;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public UpdateProductService(MohajerContext mohajerContext, IWebHostEnvironment hostingEnvironment)
        {
            this.mohajerContext = mohajerContext;
            _hostingEnvironment = hostingEnvironment;
        }





        public ResultDto UpdateProduct(ProductViewModel model, IFormFile Image, IFormFile Media, Prodoct prodoct)
        {
            int resualt = 0;
            UploadFile uploadFile = new UploadFile(_hostingEnvironment);
            int contentType = Int32.Parse(model.CountentType);
            prodoct.CountentType = contentType;
            prodoct.IsActive = true;
            prodoct.SliderShow = model.SliderShow;
            prodoct.Title = model.Title;
            prodoct.Description = model.Description;
            prodoct.Updated = DateTime.Now;
            prodoct.IsRemove = false;
           
            prodoct.Link = model.Link;
            prodoct.CategoryId = model.CategoryId;
            if (Image != null)
            {
                string imgName = prodoct.Image.Remove(0, 15);
                string folder = @$"Uploud\Images\";
                string fullPath = _hostingEnvironment.WebRootPath + @"\" + folder + imgName;

                string folderResize = @$"Uploud\ImgResized\";
                string fullPathResize= _hostingEnvironment.WebRootPath + @"\" + folderResize + imgName;
               
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                if (System.IO.File.Exists(fullPathResize))
                {
                    System.IO.File.Delete(fullPathResize);
                }
                var resultUpImg = uploadFile.UploadImage(Image);
                prodoct.Image = resultUpImg.FileNameAddress;
                prodoct.ImageResized = resultUpImg.FileNameAddressResize;
            }
            if (Media != null)
            {
                string mediaName = "";
                if (prodoct.Media !=null)
                {
                    mediaName = prodoct.Media.Remove(0, 14);
                }
                else
                {
                    mediaName = Media.FileName;
                }
                 
                string folder = @$"Uploud\Files\";
                string fullPath = _hostingEnvironment.WebRootPath + @"\" + folder+ mediaName;
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
                var resultUpFile = uploadFile.UploadMedia(Media);
                prodoct.Media = resultUpFile.FileNameAddress;
            }
            var arr = model.Tages.ToArryTag();
            foreach (var item in arr)
            {
                var result = mohajerContext.Tages.Where(p => p.Title == item.ToString().Trim()).FirstOrDefault();
                if (result == null)
                {
                    mohajerContext.Tages.Add(new Tage { Title = item.ToString().Trim() });
                    var test = mohajerContext.SaveChanges();
                }
            }
            prodoct.Tages = arr.ArryTagToString();
            mohajerContext.Entry(prodoct).State = EntityState.Modified;
            resualt = mohajerContext.SaveChanges();
            if (resualt>0)
            {
                return new ResultDto {
                    IsSuccess=true ,
                    Message="عملیات بروز رسانی موفق بود"
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
