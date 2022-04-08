using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using UserMicroServices.Services;

namespace UserMicroServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("{login}")]
        public ActionResult<User> Get(string login)
        {
            var returnSeachUser = _userService.Get(login);

            if (returnSeachUser == null)
                return BadRequest("User not Exist");

            return returnSeachUser;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User newUser)
        {
            Function function;
            if (!string.IsNullOrEmpty(newUser.Login))
            {
                try
                {
                    function = await ServiceSeachApiExisting.SeachFunctionIdInApi(newUser.Funcition.Id);

                    newUser.Funcition = function;
                    _userService.Create(newUser);

                    return CreatedAtRoute("GetUser", new { id = newUser.Id }, newUser);
                }
                catch (HttpRequestException)
                {
                    return StatusCode(503, "Service Function unavailable, start Api Function");

                }


            }

            return BadRequest("Login cannot be null");
        }

        [HttpPut("{login}")]
        public IActionResult Update(string login, User upUser)
        {
            var VerifyExistLogin = _userService.Get(login);

            if (VerifyExistLogin == null)
                return BadRequest("User not Exist");

            _userService.Update(login, upUser);

            return NoContent();
        }

        [HttpDelete("{login}")]
        public IActionResult Delete(string login)
        {
            var VerifyExistLogin = _userService.Get(login);

            if (VerifyExistLogin == null)
                return BadRequest("User not Exist, verift information");

            _userService.Remove(login);

            return NoContent();

        }

    }
}
