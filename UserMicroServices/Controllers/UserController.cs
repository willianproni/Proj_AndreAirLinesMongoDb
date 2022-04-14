using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Newtonsoft.Json;
using ProjRabbitMQLogs.Service;
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
        [AllowAnonymous]
        public ActionResult<List<User>> Get() =>
            _userService.Get();

        [HttpGet("{loginUser}", Name = "GetLogin")]
        public ActionResult<User> GetLoginUser(string loginUser)
        {
            var returnSeachUserLogin = _userService.GetLoginUser(loginUser);

            if (returnSeachUserLogin == null)
                return BadRequest("User not Exist, try again");

            return returnSeachUserLogin;
        }

        [HttpGet("LoginAndPassword", Name = "GetLoginAndPassword")]
        [Authorize]
        public ActionResult<User> GetLoginAndPassword(string login, string password)
        {
            var SeachUser = _userService.GetLoginAndPassword(login, password);

            if (SeachUser == null)
                return BadRequest("Login or Password incorrect, try again");

            return SeachUser;
        }

        [HttpPost]
        [Authorize(Roles = "Master")]

        public async Task<ActionResult<User>> Create(User newUser)
        {
            Function function;
            AddressDTO address;

            if (!string.IsNullOrEmpty(newUser.Cpf))
            {
                try
                {
                    if (!ValidateCpf.VerifyValidCpf(newUser.Cpf))
                        return Conflict("Cpf invalid, try again");

                    var verifyUser = _userService.Get(newUser.Cpf);

                    if (verifyUser != null)
                        return Conflict("User Exist");


                    function = await ServiceSeachApiExisting.SeachFunctionIdInApi(newUser.Function.Id);
                    address = await ServiceSeachViaCep.ServiceSeachCepInApiViaCep(newUser.Address.Cep);

                    newUser.Function = function;

                    newUser.Address.Cep = address.Cep;
                    newUser.Address.City = address.City;
                    newUser.Address.Street = address.State;
                    newUser.Address.District = address.District;
                    newUser.Address.Complement = address.Complement;

                    _userService.Create(newUser);

                    var newUserJson = JsonConvert.SerializeObject(newUser);
                    ServiceSeachApiExisting.LogInApiRabbit(new Log(newUser.LoginUser, null , newUserJson, "Post"));

                    return CreatedAtRoute("GetLogin", new { LoginUser = newUser.LoginUser }, newUser);
                }
                catch (HttpRequestException)
                {
                    return StatusCode(503, "Service Function unavailable, start Api Function");
                }
            }

            return BadRequest("Cpf cannot be null");
        }

        [HttpPut("{cpf}")]
        [Authorize(Roles = "Master")]
        public IActionResult Update(string cpf, User upUser)
        {
            var SeachUser = _userService.Get(cpf);

            if (SeachUser == null)
                return BadRequest("User not Exist");

            _userService.Update(cpf, upUser);

            var updateUserJson = JsonConvert.SerializeObject(upUser);
            var oldUser = JsonConvert.SerializeObject(SeachUser);
            ServiceSeachApiExisting.LogInApiRabbit(new Log(upUser.LoginUser, oldUser, updateUserJson, "Update"));

            return NoContent();
        }

        [HttpDelete("{cpf}")]
        [Authorize(Roles = "Master")]


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
