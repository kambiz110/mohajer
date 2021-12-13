using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Mohajer.Entity
{
    public class Nazarat
    {
        [Key]
        public long Id { get; set; }
        public string Subject { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public bool IsActive { get; set; } = false;
        public int Category { get; set; }
        public string Ip { get; set; }

        [DataType(DataType.Date)]
        public DateTime AtInserted { get; set; }
        public DateTime? Removed { get; set; } 
        public bool? IsRemove { get; set; } = false;



    }
}
