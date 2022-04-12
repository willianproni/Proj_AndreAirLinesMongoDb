using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AirportMicroServices.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Model;
using Model.DataModel;
using Newtonsoft.Json;
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
        [AllowAnonymous]
        public ActionResult<List<Airport>> Get() => //Função de buscar todos os Aeroportos
            _airportService.Get();                  // ###

        [HttpGet("{iata}", Name = "GetAirport")]
        [Authorize]
        public ActionResult<Airport> GetSeachAirportIata(string iata) //Responsável por trazer uma dado especifico pelo CodeIata
        {
            var SeachAirport = _airportService.GetAirport(iata); //Verifica se o Aeroporto busca existe

            if (SeachAirport == null)                                                                      //Se o aeroporto não for encontrado -->
                return BadRequest("Airport does not exist in the database, check the data and try again"); //<-- Retorna a mensagem

            return SeachAirport;
        }

        [HttpPost] //Responsável por criar um novo Dado Aeroporto na Api
        [Authorize(Roles = "Master")]
        public async Task<ActionResult<Airport>> Create(Airport newAirport)
        {
            AddressDTO addressAirport;
            AirportData InfoAirportData;

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

                var newAirportJson = JsonConvert.SerializeObject(newAirport); //Converte o novo aeroporto em arquivo Json
                PostLogApi.PostLogInApi(new Log(newAirport.LoginUser, null, newAirportJson, "Post")); //Chama o serviço de cadastrar Log

                return CreatedAtRoute("GetAirport", new { id = newAirport.Id.ToString() }, newAirport); //Retorna a os dados do novo aeroporto inserido no do Post da api.

            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "Service AirportDapper unavailable, start Api AiportDapper and try again"); //Se a api 'AirportDataDapper' não estiver iniciada, o programa vai apresentar esse erro
            }

        }

        [HttpPut("{iata}")] //Responsável por deletar um dado da Api referente ao CodeIata inserido
        [Authorize(Roles = "Master")]
        public IActionResult Update(string iata, Airport upAirport)
        {

            var SeachAirport = _airportService.GetAirport(iata); //Verifica se o aeroporto existe no banco de dados

            if (SeachAirport == null)                                                                       //Se o aeroporto não for encontrado -->
                return BadRequest("Airport does not exist in the database, check the data and try again"); //<-- Retorna a mensagem

            _airportService.Uptade(iata, upAirport); //Realiza o Serviço de Update

            var updateAirportJson = JsonConvert.SerializeObject(upAirport);
            var oldAirportJson = JsonConvert.SerializeObject(SeachAirport); 
            PostLogApi.PostLogInApi(new Log(upAirport.LoginUser, oldAirportJson, updateAirportJson, "Update")); //Chama o serviço de cadastrar Log

            return NoContent();
        }

        [HttpDelete("{iata}")] //Deleta um Airport pelo Código da iata
        [Authorize(Roles = "Master")]
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
