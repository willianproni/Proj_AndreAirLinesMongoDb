using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.DataModel;

namespace ReadAndInsertDataAirport.Util
{
    public interface IAirportRepository
    {
        bool Add(AirportData airport);

        List<AirportData> GetAll();
    }
}
