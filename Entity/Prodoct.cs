using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class Prodoct :BaseEntity
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string ImageResized { get; set; }
        public string Media { get; set; }
        public int CountentType { get; set; }
        public bool SliderShow { get; set; }

        public string Link { get; set; }

        public string Tages { get; set; }
        public long UserId { get; set; }
        public  User User { get; set; }
        public long CategoryId { get; set; }
        public Category Category { get; set; }
    }

}
