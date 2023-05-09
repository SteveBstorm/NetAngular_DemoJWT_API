using Dapper;
using DemoJWT_DAL.Entities;
using DemoJWT_DAL.Repos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoJWT_DAL.Services
{
    public class UserService : IUserService
    {
        private IDbConnection _connection;

        public UserService(IDbConnection connection)
        {
            _connection = connection;
        }

        public bool RegisterUser(string email, string pwd, string nickname)
        {
            string sql = "RegisterUser";
            var param = new { email = email, pwd = pwd, nickname = nickname };
            return _connection.Execute(sql, param, commandType: CommandType.StoredProcedure) > 0;
        }

        public User Login(string email, string pwd)
        {
            string sql = "LoginUser";
            var param = new { email = email, pwd = pwd };
            return _connection.QueryFirstOrDefault<User>(sql, param, commandType: CommandType.StoredProcedure);
        }

        public void SwitchRole(int id)
        {
            string sql = "SwitchRole";
            var param = new { id = id };
            _connection.Execute(sql, param, commandType: CommandType.StoredProcedure);
        }

        public IEnumerable<User> GetAll()
        {
            string sql = "SELECT * FROM Users";
            return _connection.Query<User>(sql);
        }
    }
}
