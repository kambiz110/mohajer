using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{
    public class Order
    {
        [Key]
        public long Id { get; set; }
        public string Full_Name { get; set; }
        public string Subject { get; set; }
        public string Description  { get; set; }
        public string Phone { get; set; }
        [DataType(DataType.Date)]
        public DateTime AtInserted { get; set; }
        public string Ip { get; set; }

        public DateTime? Removed { get; set; }


        public bool? IsRemove { get; set; } = false;
    }
}
