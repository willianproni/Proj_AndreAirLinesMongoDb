using System.Threading.Tasks;
using AuthenticationJWS.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace AuthenticationJWS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginJWSController : ControllerBase
    {
        [HttpPost]
        [Route("login")]
        public async Task<ActionResult<dynamic>> AuthenticateAsync(User model)
        {
            var user = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(model.LoginUser);

            if (user == null)
                return NotFound("User or password invalid, try again");

            var token = TokenService.GenerateToken(user);

            user.Password = "";

            return new
            {
                user = user,
                token = token
            };
        }
    }
}
