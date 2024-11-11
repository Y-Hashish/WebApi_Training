using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;

        public IConfiguration Config { get; }

        public AccountController(UserManager<ApplicationUser> userManger, IConfiguration config)
        {
            this.userManger = userManger;
            Config = config;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDto UserFromRequest)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                user.UserName = UserFromRequest.UserName;
                user.Email = UserFromRequest.Email;
                IdentityResult result = await userManger.CreateAsync(user, UserFromRequest.Password);
                if (result.Succeeded)
                {
                    return Ok("Created");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("Password", item.Description);
                    }
                }
            }
            return BadRequest(ModelState);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginDto userFromRequest)
        {
            if(ModelState.IsValid)
            {
               ApplicationUser userFromDb = 
                   await userManger.FindByNameAsync(userFromRequest.UserName);
                if(userFromDb != null)
                {
                   bool found=
                        await userManger.CheckPasswordAsync(userFromDb, userFromRequest.Password);
                    if (found)
                    {

                        //create token ==> design token ==> generate token


                        // create SiginingCredential
                        var key =
                            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Config["JWT:Key"]));
                        var SiginingCred = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        //create claims 
                        List<Claim> UserClaims = new List<Claim>();
                        // Token generated Id change 
                        UserClaims.Add(new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()));
                        // the rest of claims 
                        UserClaims.Add(new Claim(ClaimTypes.NameIdentifier, userFromDb.UserName));
                        UserClaims.Add(new Claim(ClaimTypes.Name, userFromDb.UserName));

                        var UserRole = await userManger.GetRolesAsync(userFromDb);
                        foreach (var item in UserRole)
                        {
                            UserClaims.Add(new Claim(ClaimTypes.Role, item));
                        }



                        //design token
                        JwtSecurityToken myToken = new JwtSecurityToken(
                            issuer: Config["Jwt:Issuer"],
                            audience: Config["JWT:AudienceIp"],
                            expires: DateTime.Now.AddHours(1),
                            claims: UserClaims,
                            signingCredentials: SiginingCred
                            );


                        //generate token 

                        return Ok(new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(myToken),
                            expiration = DateTime.Now.AddHours(1),
                        });
                    }
                    else ModelState.AddModelError("UserName", "UserName or Password id wrong");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
