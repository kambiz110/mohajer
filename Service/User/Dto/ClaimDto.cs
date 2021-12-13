using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Mohajer.Service.Admin.Product.Dto
{
    public class ClaimDto
    {
        public JwtSecurityToken JwtSecurityToken { get; set; }
        public ClaimsPrincipal principal { get; set; }
       public AuthenticationProperties properties { get; set; }

    }
}
