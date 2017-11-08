using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.Models;

namespace WhyNotRun.DAO
{
    public class PublicationDAO : ContextAsyncDAO<Publication>
    {
        public PublicationDAO() : base()
        {

        }

        /// <summary>
        /// Lista todas as publicações
        /// </summary>
        /// <returns>Lista de publicações</returns>
        public async Task<List<Publication>> ListPublications()
        {
            var filter = FilterBuilder.Exists(a => a.DeletedAt, false);
            return await Collection.Find(filter).ToListAsync();
        }

        /// <summary>
        /// Cria uma publicação
        /// </summary>
        /// <param name="publication">Publicação a ser criada</param>
        /// <returns></returns>
        public async Task<Publication> CreatePublication(Publication publication)
        {
            await Collection.InsertOneAsync(publication);
            return publication;
        }








        

    }
}