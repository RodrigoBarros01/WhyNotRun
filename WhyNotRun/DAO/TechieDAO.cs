using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.Models;

namespace WhyNotRun.DAO
{
    public class TechieDAO : ContextAsyncDAO<Techie>
    {
        public TechieDAO() : base()
        {

        }

        /// <summary>
        /// Adiciona um nos posts da tecnologia
        /// </summary>
        /// <param name="techie">Tecnologia a receber o post adicionado</param>
        public async Task<bool> AddPost(Techie techie)
        {
            var filter = FilterBuilder.Eq(a => a.Id, techie.Id) & FilterBuilder.Exists(a => a.DeletedAt, false);
            var update = UpdateBuilder.Set(a => a.Posts, techie.Posts++);
            var retorno = await Collection.UpdateOneAsync(filter, update);
            return retorno.IsAcknowledged;
        }




    }
}