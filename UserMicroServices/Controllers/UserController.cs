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

        [HttpGet("{loginUser}", Name = "GetLogin")]
        public ActionResult<User> GetLogin(string loginUser)
        {
            var returnSeachUserLogin = _userService.GetLogin(loginUser);

            if (returnSeachUserLogin == null)
                return BadRequest("User not Exist, try again");

            return returnSeachUserLogin;
        }

        [HttpPost]
        public async Task<ActionResult<User>> Create(User newUser)
        {
            Function function;
            AddressDTO address;
            User permissionUser;

            try
            {
                permissionUser = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(newUser.LoginUser);

                if (permissionUser.Funcition.Id != "1")
                    return BadRequest("Access blocked, need manager permission");
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service User unavailable, start Api");
            }

            if (!string.IsNullOrEmpty(newUser.Cpf))
            {
                try
                {
                    if (!ValidateCpf.VerifyValidCpf(newUser.Cpf))
                        return Conflict("Cpf invalid, try again");

                    var verifyUser = _userService.Get(newUser.Cpf);

                    if (verifyUser != null)
                        return Conflict("User Exist");


                    function = await ServiceSeachApiExisting.SeachFunctionIdInApi(newUser.Funcition.Id);
                    address = await ServiceSeachViaCep.ServiceSeachCepInApiViaCep(newUser.Address.Cep);

                    newUser.Funcition = function;

                    newUser.Address.Cep = address.Cep;
                    newUser.Address.City = address.City;
                    newUser.Address.Street = address.State;
                    newUser.Address.District = address.District;
                    newUser.Address.Complement = address.Complement;

                    _userService.Create(newUser);

                    return CreatedAtRoute("GetUser", new { cpf = newUser.Cpf }, newUser);
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
