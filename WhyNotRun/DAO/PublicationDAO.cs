using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.BO;
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
        public async Task<List<Publication>> ListPublications(int page)
        {
            var filter = FilterBuilder.Exists(a => a.DeletedAt, false);
            var sort = SortBuilder.Descending(a => a.DateCreation);
            var projection = ProjectionBuilder.Slice(a => a.Comments, 0, 3);


            return await Collection
                .Find(filter)
                .Sort(sort)
                .Skip((page - 1) * UtilBO.QUANTIDADE_PAGINAS)
                .Limit(UtilBO.QUANTIDADE_PAGINAS)
                .Project<Publication>(projection)
                .ToListAsync();
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

        public async Task<List<Comment>> SeeMoreComments(ObjectId publicationId, ObjectId lastCommentId, int limit)
        {

            var match = new BsonDocument
            {
                {
                    "deletedAt",
                    new BsonDocument {
                        { "$exists", false}
                    }
                },
                {
                    "_id",
                    publicationId
                }
            };

            var projection = new BsonDocument
            {
                {"comments", 1 },
                {"_id", 0 }
            };

            var mathComments = new BsonDocument
            {
                {
                    "comments._id",
                    new BsonDocument
                    {
                        {"$lt", lastCommentId }
                    }
                }
            };

            var result = await 
                Collection
                .Aggregate()
                .Match(match)
                .Unwind(a => a.Comments)
                .Project(projection)
                .Match(mathComments)
                .Limit(limit)
                .ToListAsync();

            List<Comment> comentarios = new List<Comment>();
            foreach (var item in result)
            {
                comentarios.Add(BsonSerializer.Deserialize<Comment>(item["comments"].AsBsonDocument));
            }
            return comentarios.ToList();
        }

        


    }
}