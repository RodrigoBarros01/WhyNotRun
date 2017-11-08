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

        /// <summary>
        /// Curti uma publicação
        /// </summary>
        /// <param name="userId">Usuario que curtiu</param>
        /// <param name="publicationId">publicação curtida</param>
        public async Task<bool> Like(ObjectId userId, ObjectId publicationId)
        {
            var filter = FilterBuilder.Eq(a => a.Id, publicationId) & FilterBuilder.Exists(a => a.DeletedAt, false);
            var update = UpdateBuilder.Push(a => a.Likes, userId).Pull(a => a.Dislikes, userId);

            var resultado = await Collection.UpdateOneAsync(filter, update);
            if (resultado.IsModifiedCountAvailable && resultado.IsAcknowledged)
            {
                return resultado.ModifiedCount == 1;
            }
            return false;
        }

        /// <summary>
        /// Da dislike em uma publicação
        /// </summary>
        /// <param name="userId">Usuario que deu dislike</param>
        /// <param name="publicationId">publicação que recebeu dislike</param>
        public async Task<bool> Dislike(ObjectId userId, ObjectId publicationId)
        {
            var filter = FilterBuilder.Eq(a => a.Id, publicationId) & FilterBuilder.Exists(a => a.DeletedAt, false);
            var update = UpdateBuilder.Push(a => a.Dislikes, userId).Pull(a => a.Likes, userId);

            var resultado = await Collection.UpdateOneAsync(filter, update);
            if (resultado.IsModifiedCountAvailable && resultado.IsAcknowledged)
            {
                return resultado.ModifiedCount == 1;
            }
            return false;
        }
        
        /// <summary>
        /// Busca uma publicação
        /// </summary>
        /// <returns>Lista de publicações</returns>
        public async Task<Publication> SearchPublicationById(ObjectId publicationId)
        {
            var filter = FilterBuilder.Exists(a => a.DeletedAt, false) & FilterBuilder.Eq(a => a.Id, publicationId);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }



    }
}