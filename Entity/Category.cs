using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime? Removed { get; set; }
        public bool? IsRemove { get; set; } = false;
        public int? ParentCategoryId { get; set; }
        public Category ParentCategory { get; set; }
        public List<Category> Children { get; set; }
        public ICollection<Prodoct> Prodocts { get; set; }

    }
}
