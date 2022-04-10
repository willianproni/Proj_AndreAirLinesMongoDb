using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AirportMicroServices.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DataModel;
using Services;

namespace AirportMicroServices.Controllers
{
    [Route("api/[controller]")] //Rota que a api vai receber "api/airport
    [ApiController]
    public class AirportController : ControllerBase
    {
        private readonly AirportService _airportService; //Cria um atribulo do tipo 'AirportService'

        public AirportController(AirportService airportService) //Contrutor 'AirportController'
        {
            _airportService = airportService; //Recebimento de dados
        }

        [HttpGet] //Responsável por trazer todos os Aeroportos Cadastrado
        public ActionResult<List<Airport>> Get() => //Função de buscar todos os Aeroportos
            _airportService.Get();                  // ###

        [HttpGet("{iata}", Name = "GetAirport")]
        public ActionResult<Airport> GetSeachAirportIata(string iata) //Responsável por trazer uma dado especifico pelo CodeIata
        {
            var SeachAirport = _airportService.GetAirport(iata); //Verifica se o Aeroporto busca existe

            if (SeachAirport == null)                                                                      //Se o aeroporto não for encontrado -->
                return BadRequest("Airport does not exist in the database, check the data and try again"); //<-- Retorna a mensagem

            return SeachAirport;
        }

        [HttpPost] //Responsável por criar um novo Dado Aeroporto na Api
        public async Task<ActionResult<Airport>> Create(Airport newAirport)
        {
            AddressDTO addressAirport;
            AirportData InfoAirportData;
            User permissionUser;
            try
            {
                permissionUser = await ServiceSeachApiExisting.SeachUserInApiByLoginUser(newAirport.LoginUser); //Verifica a Api User e retorna a informação referente ao LoginUser

                if (permissionUser.Funcition.Id != "1") //Verifica se a função tem acesso a Post Airport
                    return BadRequest("Access blocked, need manager permission"); //Ser não ter acesso retorna a BadRequest
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service User unavailable, start Api"); //Se a API User estiver desligada retorna o seguinte erro
            }

            try
            {
                if (_airportService.VerifyCodeIata(newAirport.CodeIATA)) //Verifica se o Aeroporto já existe no database;
                    return Conflict("Airport already registered in the database");

                addressAirport = await ServiceSeachViaCep.ServiceSeachCepInApiViaCep(newAirport.Address.Cep); //Serviço de verificação pelo Cep informado --> Verificado no ViaCep

                InfoAirportData = await ServiceSeachApiExisting.SeachAirportDataSqlIdApi(newAirport.CodeIATA);//Serviço de verificação pelo CodeIata iformado
                                                                                                              //--> Verificano na Api do MicroServiço do 'AirportDataDaper'                                                    
                                                                                                              //Serviço de Busca Usando o ViaCep
                newAirport.Address.Cep = addressAirport.Cep; //O cep do novo Aeroporto vai receber o cep vindo do ViaCep
                newAirport.Address.State = addressAirport.State; //O State do novo Aeroporto vai receber o cep vindo do ViaCep
                newAirport.Address.Street = addressAirport.Street; //A Street do novo Aeroporto vai receber o cep vindo do ViaCep
                newAirport.Address.Complement = addressAirport.Complement; //O Complement do novo Aeroporto vai receber o cep vindo do ViaCep

                //Seriviço de Busca Usando o MicroServiço AirportDataDapper que está conectado ao banco com os Dados de Vários Aeroportos
                newAirport.Address.Continent = InfoAirportData.Continent; //O Continent vai receber a informação vindo do MicroSerivço 'AirportDataDapper'
                newAirport.Address.Country = InfoAirportData.Country; //O Country vai receber a informação vindo do MicroSerivço 'AirportDataDapper'
                newAirport.Address.City = InfoAirportData.City; //A City vai receber a informação vindo do MicroSerivço 'AirportDataDapper'

                _airportService.Create(newAirport); //Cria um novo Aeroporto no database

                return CreatedAtRoute("GetAirport", new { id = newAirport.Id.ToString() }, newAirport); //Retorna a os dados do novo aeroporto inserido no do Post da api.

            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service AirportDapper unavailable, start Api AiportDapper and try again"); //Se a api 'AirportDataDapper' não estiver iniciada, o programa vai apresentar esse erro
            }

        }

        [HttpPut("{iata}")] //Responsável por deletar um dado da Api referente ao CodeIata inserido
        public IActionResult Update(string iata, Airport upAirport)
        {
            var SeachAirport = _airportService.GetAirport(iata); //Verifica se o aeroporto existe no banco de dados

            if (SeachAirport == null)                                                                       //Se o aeroporto não for encontrado -->
                return BadRequest("Airport does not exist in the database, check the data and try again"); //<-- Retorna a mensagem

            _airportService.Uptade(iata, upAirport); //Realiza o Serviço de Update

            return NoContent();
        }

        [HttpDelete("{iata}")] //Deleta um Airport pelo Código da iata
        public IActionResult Delete(string iata)
        {
            var SeachAirport = _airportService.GetAirport(iata); //Verifica se o aeroporto existe no banco de dados

            if (SeachAirport == null)                                                                       //Se o aeroporto não for encontrado -->
                return BadRequest("Airport does not exist in the database, check the data and try again"); //<-- Retorna a mensagem

            _airportService.Remove(SeachAirport.CodeIATA); //Realiza o serviço de exclusão

            return NoContent();
        }

    }
}
