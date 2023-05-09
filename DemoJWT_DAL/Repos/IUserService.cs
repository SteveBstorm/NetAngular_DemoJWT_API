using DemoJWT_DAL.Entities;

namespace DemoJWT_DAL.Repos
{
    public interface IUserService
    {
        User Login(string email, string pwd);
        bool RegisterUser(string email, string pwd, string nickname);
        void SwitchRole(int id);
        IEnumerable<User> GetAll();
    }
}