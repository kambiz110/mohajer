using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Product.Dto
{
    public class ProductForHomePageDto
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string ImageResized { get; set; }
        public string Image { get; set; }
        public int CountentType { get; set; }
        public int CommentCount { get; set; } = 0;
        public DateTime Updated { get; set; }
        public Mohajer.Entity.User user { get; set; }
    }
}
