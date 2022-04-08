using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using Model.DataModel;
using ReadAndInsertDataAirport.Config;

namespace AirportDataDapper.Reposity
{
    public class AirportDataReposity : IAirpotyDateReposity
    {

        private static string _conection; //Criando a string que vai receber o retorno do banco.

        public AirportDataReposity() //Gerando Contrutor
        {
            _conection = DataBaseConfiguration.Get(); //Definindo que a string(_conection) é o retorno do DataBaseConfiguration.Get().
        }

        public bool Add(AirportData airport) //Dapper Inserir Informações.
        {
            bool status = false; //Inicia um Status inicial, que poderá ser alterado conforme a execução.

            using (var dataBase = new SqlConnection(_conection)) //Using "traduz" um metodo Try/Finish
            {
                dataBase.Open(); //Iniciar(Abrir) Banco de dados.
                dataBase.Execute(AirportData.INSERT, airport);//Executar o Dapper de INSERIR, daodos inseridos.
                status = true; //Se o Execute funcionar sem conflitos o status é alterado para true.
            }
            return status; //Retorno do Status
        }

        public List<AirportData> GeAll() //Busca todos o Aeroporto
        {
            using (var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                var airport = dataBase.Query<AirportData>(AirportData.GETALL); //Faz a consulta no AirportData e pega todos os dados encontrados.
                return (List<AirportData>)airport; //Retorna os dados encontrados pelo Get.
            }
        }

        public AirportData Get(string id) //Buscar um Determinado Dado de Aeroporto pelo ID
        {
            using (var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                var airportSeach = dataBase.QueryFirstOrDefault<AirportData>(AirportData.GETID, new { id = id}); //O Var airportSheach vai receber a consulta pelo ID no AirportData.
                                                                                       //E retornar a informação se for encontrado .
                return (AirportData)airportSeach; //Retorno do Aeroporto encontrado.
            }
        }

        public void Delete(string id) //Delete um informação do Banco pelo Id informado
        {
            using (var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.DELETE, new {id = id}); //Executa a função DELETE da dapper, que tem como finalidade Deletar dados
            }
        }

        public void Update(AirportData airport) //Atualiza as informações de uma determina coluna pelo Id iformado
        {
            using (var dataBase = new SqlConnection(_conection))
            {
                dataBase.Open();
                dataBase.Execute(AirportData.UPDATE, airport); //Informa o ID e realiza o Update
            }
        }
    }
}
