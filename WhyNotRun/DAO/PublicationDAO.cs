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
            var filter = FilterBuilder.Exists(a => a.DeletedAt, false);// & SortBuilder.Descending(a => a.DateCreation);
            

            return (await Collection.Find(filter).ToListAsync()).OrderByDescending(a => a.DateCreation).ToList();
        }

        /// <summary>
        /// Cria uma publicação
        /// </summary>
        /// <param name="publication">Publicação a ser criada</param>
        /// <returns></returns>
        public async Task CreatePublication(Publication publication)
        {
            await Collection.InsertOneAsync(publication);
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
            
            return resultado.IsModifiedCountAvailable && resultado.IsAcknowledged && resultado.ModifiedCount == 1;
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
            return resultado.IsModifiedCountAvailable && resultado.IsAcknowledged && resultado.ModifiedCount == 1;
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
        
        /// <summary>
        /// Adiciona um comentario a uma publicação
        /// </summary>
        /// <param name="comment">Comentario a ser adicionado</param>
        /// <param name="publicationId">publicação que vai receber o comentario</param>
        public async Task<bool> AddComment(Comment comment, ObjectId publicationId)
        {
            var filter = FilterBuilder.Eq(a => a.Id, publicationId) & FilterBuilder.Exists(a => a.DeletedAt, false);
            var update = UpdateBuilder.Push(a => a.Comments, comment);

            var resultado = await Collection.UpdateOneAsync(filter, update);

            return resultado.IsModifiedCountAvailable && resultado.IsAcknowledged && resultado.ModifiedCount == 1;

        }

        ///// <summary>
        ///// Procura uma lista de publicações
        ///// </summary>
        ///// <param name="ids"></param>
        ///// <returns></returns>
        //public async Task<List<Publication>> SearchPublications(List<ObjectId> ids)
        //{
        //    var filter = FilterBuilder.Exists(a => a.DeletedAt, false) & FilterBuilder.In(a => a.Id, ids);
        //    return await Collection.Find(filter).ToListAsync();
        //}


    }
}