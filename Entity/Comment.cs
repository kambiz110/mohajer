using System;
using System.Collections.Generic;

using System.ComponentModel.DataAnnotations;

using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Entity
{

    public class Comment
    {
        [Key]
        public long Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CommentMsg { get; set; }
        public string AnserMsg { get; set; }
        public long ProdoctId { get; set; }
        public Prodoct Prodoct { get; set; }
        public DateTime? Removed { get; set; }
        public bool? IsRemove { get; set; } = false;
        public bool? IsActive { get; set; } = false;
        public string Ip { get; set; }

        [DataType(DataType.Date)]
        public DateTime AtInserted { get; set; }
    }

}
