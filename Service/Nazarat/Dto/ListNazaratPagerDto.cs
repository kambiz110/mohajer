using Mohajer.Useful.Result;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.Nazarat.Dto
{
    public class ListNazaratPagerDto
    {
        public PagerDto PagerDto { get; set; }
        public List<NazaratViewDto> nazarShows { get; set; }
    }
}
