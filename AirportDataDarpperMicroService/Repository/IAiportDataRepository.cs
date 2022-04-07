using System.Collections.Generic;
using Model.DataModel;

namespace AirportDataDarpperMicroService.Repository
{
    public interface IAiportDataRepository
    {
        bool Add(AirportData airport);
        List<AirportData> GetAll();
        AirportData Get(string id);
        void Remove(string id);
        void Update(AirportData id);
    }
}
