
using Microsoft.IdentityModel.Tokens;
using Mohajer.Entity;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
namespace Mohajer
{
    public class TokenProvider
    {
        private readonly MohajerContext _mohajerContext;
        public TokenProvider(MohajerContext mohajerContext)
        {
            this._mohajerContext = mohajerContext;
        }
        public string LoginUser(long UserID, string Password)
        {
            //Get user details for the user who is trying to login - JRozario
           var user = _mohajerContext.Users.SingleOrDefault(x => x.Id == UserID);
           
            //Authenticate User, Check if its a registered user in DB  - JRozario
         //   if (user == null)
           //     return null;

            //If its registered user, check user password stored in DB - JRozario
            //For demo, password is not hashed. Its just a string comparison - JRozario
            //In reality, password would be hashed and stored in DB. Before comparing, hash the password - JRozario
            if (Password == user.Password)
            {
      
                //Authentication successful, Issue Token with user credentials - JRozario
                //Provide the security key which was given in the JWToken configuration in Startup.cs - JRozario
                var key = Encoding.ASCII.GetBytes("YourKey-2374-OFFKDI940NG7:56753253-tyuw-5769-0921-kfirox29zoxv");
                //Generate Token for user - JRozario
                var JWToken = new JwtSecurityToken(
                    issuer: "http://localhost:45092/",
                    audience: "http://localhost:45092/",
                    claims: GetUserClaims(user),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(DateTime.Now.AddHours(1.0)).DateTime,
                    //Using HS256 Algorithm to encrypt Token - JRozario
                    signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                );
                var token = new JwtSecurityTokenHandler().WriteToken(JWToken);
                return token;
            }
            else
            {
                return null;
            }
        }

        //Using hard coded collection list as Data Store for demo purpose. In reality, User data comes from Database or some other Data Source - JRozario


        //Using hard coded collection list as Data Store for demo. In reality, User data comes from Database or some other Data Source - JRozario
        private IEnumerable<Claim> GetUserClaims(User user)
        {
            IEnumerable<Claim> claims = new Claim[]
                    {
                    new Claim(ClaimTypes.Name, user.First_Name + " " + user.Last_Name),
                    new Claim("USERID", user.Id+""),
                    new Claim("EMAILID",  user.Email ),
                    new Claim("PHONE", value: user.Phone),
                    new Claim("ROLE", value: user.Role.ToUpper())
                    };
            return claims;
        }
    }
}
