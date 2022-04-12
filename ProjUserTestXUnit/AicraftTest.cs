using System;
using System.Collections.Generic;
using System.Linq;
using AircraftMicroService.Services;
//using AircraftMicroService.Services;
using Model;
using MongoDB.Driver;
using Moq;
using UserMicroService.Util;
using UserMicroServices.Controllers;
using UserMicroServices.Services;
using Xunit;

namespace ProjUserTestXUnit
{
    public class AicraftTest
    {

        private List<Aircraft> _allAircraft;
        private void InitializeDataBase()
        {
            _allAircraft = new List<Aircraft>();
            _allAircraft.Add(new Aircraft() { Id = "1", Name = "Napoleão", Capacity = 300, LoginUser = "willian" });
            _allAircraft.Add(new Aircraft() { Id = "2", Name = "Cabral", Capacity = 200 , LoginUser = "willianproni" });
            _allAircraft.Add(new Aircraft() { Id = "3", Name = "OldWhite", Capacity = 100, LoginUser = "willianproni" });
        }



        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            var mock = new Mock<IArcraftService>();
            mock.Setup(x => x.Get()).Returns(_allAircraft);

            IArcraftService aircraftService = mock.Object;

            var items = aircraftService.Get();
            var count = items.Count;

            Assert.Equal(3, count);

        }

/*        [Fact]
        public void GetId(string id)
        {
            InitializeDataBase();

            string aircraftId = "2";
            var mock = new Mock<AircraftService>();
            mock.Setup(x => x.Get(id)).Returns();
        }*/
    }
}
