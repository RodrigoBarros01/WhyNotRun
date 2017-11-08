using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.Models;

namespace WhyNotRun.DAO
{

    public class UserDAO : ContextAsyncDAO<User>
    {
        public UserDAO() : base()
        {

        }

        public async Task<User> CreateUser(User user)
        {
            await Collection.InsertOneAsync(user);
            return user;
        }

        public async Task<User> SearchUserPerId(ObjectId id)
        {
            var filter = FilterBuilder.Eq(a => a.Id, id)
                & FilterBuilder.Exists(a => a.DeletedAt, false);

            return await Collection.Find(filter).FirstOrDefaultAsync();
        }
    }

}