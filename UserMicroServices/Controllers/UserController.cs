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

        [HttpGet("{cpf}")]
        public ActionResult<User> Get(string cpf)
        {
            var returnSeachUser = _userService.Get(cpf);

            if (returnSeachUser == null)
                return BadRequest("User not Exist");

            return returnSeachUser;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User newUser)
        {
            Function function;
            if (!string.IsNullOrEmpty(newUser.Cpf))
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

            return BadRequest("Cpf cannot be null");
        }

        [HttpPut("{cpf}")]
        public IActionResult Update(string cpf, User upUser)
        {
            var VerifyExistCpf = _userService.Get(cpf);

            if (VerifyExistCpf == null)
                return BadRequest("User not Exist");

            _userService.Update(cpf, upUser);

            return NoContent();
        }

        [HttpDelete("{cpf}")]
        public IActionResult Delete(string cpf)
        {
            var VerifyExistCpf = _userService.Get(cpf);

            if (VerifyExistCpf == null)
                return BadRequest("User not Exist, verift information");

            _userService.Remove(cpf);

            return NoContent();

        }

    }
}
