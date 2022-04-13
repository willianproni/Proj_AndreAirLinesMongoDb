using System;
using System.Collections.Generic;
using Model;
using Moq;
using UserMicroService.Util;
using UserMicroServices.Services;
using Xunit;

namespace ProjUserTestUnit
{
    public class UserTest
    {
        public class ProjMongoDBApiSettings : IUserDatabase
        {
            public string UserCollectionName { get; set; } = "User";
            public string ConnectionString { get; set; } = "mongodb://localhost:27017";
            public string DatabaseName { get; set; } = "db_User";
        }

        public UserService InitializeDataBase()
        {
            var settings = new ProjMongoDBApiSettings();
            UserService userService = new(settings);
            return userService;
        }

        [Fact]
        public void GetAll()
        {
            var userService = InitializeDataBase();
            var users = userService.Get();
        }

        [Fact]
        public void GetUserCpf()
        {
            var userService = InitializeDataBase();
            var user = userService.Get("44954343810");
            if (user == null)
                user = new User();
            Assert.Equal("44954343810", user.Cpf);
        }

        [Fact]
        public void Create()
        {
            var userService = InitializeDataBase();
            User newUser = new User()
            {
                Name = "André",
                Cpf = "33346952045",
                Phone = "(16) 976544534",
                Email = "wifepro@hotmail.com",
                Login = "arthur01",
                Password = "arthur123",
                Sector = "Administrativo"
            };

            userService.Create(newUser);
            var userReturn = userService.Get("33346952045");
            Assert.Equal("33346952045", userReturn.Cpf);

        }

        [Fact]
        public void Update()
        {
            var userService = InitializeDataBase();

            User updateUser = new User()
            {
                Id = "6255c6ef4d7da144fc170ade",
                Name = "Alberto",
                Cpf = "88170507090",
                Phone = "(16) 976544534",
                Email = "wifepro@hotmail.com",
                Login = "arthur01",
                Password = "arthur123",
                Sector = "Administrativo"
            };

            userService.Update("88170507090", updateUser);
            var userReturn = userService.Get("88170507090");
            Assert.Equal("88170507090", userReturn.Cpf);
        }

        [Fact]
        public void Delete()
        {
            var userService = InitializeDataBase();
            userService.Remove("20192336061");

            var user = userService.Get("20192336061");
            Assert.Null(user);
        }

       
    }
}
