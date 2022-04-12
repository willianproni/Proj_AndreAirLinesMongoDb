using System.Collections.Generic;
using Model;

namespace UserMicroServices.Services
{
    public interface IUserService
    {
        List<User> Get();
        User Get(string cpf);
        User GetLoginUser(string loginUser);
        User GetLoginAndPassword(string login, string password);
        User Create(User newUser);
        void Update(string cpf, User updateUser);
        void Remove(string cpf);

    }
}
