using System.Collections.Generic;
using Model.DataModel;

namespace AirportDataDapper.Reposity
{
    public interface IAirpotyDateReposity
    {
        bool Add(AirportData airport); //Adiconar.
        List<AirportData> GeAll(); //listar Tudo.
        AirportData Get(string id); //buscar um unico dado.
        void Update(AirportData airport); //Atualizar Dado informado.
        void Delete(string id); //Deletar dado informado.
    }
}
