using System.Collections.Generic;
using Model;

namespace AircraftMicroService.Services
{
    public interface IArcraftService
    {
        List<Aircraft> Get();
        Aircraft Get(string id);
        Aircraft GetNameAircraft(string name);
        Aircraft Create(Aircraft newAircraft);
        void Update(string id, Aircraft updateAircraft);
        void Remove(string id);

    }
}