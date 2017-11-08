using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.DAO;
using WhyNotRun.Models;

namespace WhyNotRun.BO
{
    public class UserBO
    {
        private const string PICTURE_DEFAULT = "picture-pattern.jpg";

        private UserDAO _userDao;

        public UserBO()
        {
            _userDao = new UserDAO();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = ObjectId.GenerateNewId();
            user.Password = user.Password;
            return await _userDao.CreateUser(user);
        }

        public async Task<User> SearchUserPerId(ObjectId id)
        {
            return await _userDao.SearchUserPerId(id);
        }

    }
}