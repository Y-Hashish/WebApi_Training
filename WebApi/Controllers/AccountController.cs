using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApi.DTO;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManger;

        public AccountController(UserManager<ApplicationUser> userManger)
        {
            this.userManger = userManger;
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
                        //create token
                    }
                    else ModelState.AddModelError("UserName", "UserName or Password id wrong");
                }
            }
            return BadRequest(ModelState);
        }
    }
}
