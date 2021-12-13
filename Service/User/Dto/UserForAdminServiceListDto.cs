using Mohajer.Useful.Result;
using Mohajer.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mohajer.Service.User.Dto
{
    public class UserForAdminServiceListDto
    {
        public PagerDto PagerDto { get; set; }
        public List<UserShowListViewModel> userShows { get; set; }
    }
}
