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
    public class PublicationBO
    {
        private PublicationDAO _publicationDao;

        public PublicationBO()
        {
            _publicationDao = new PublicationDAO();
        }

        /// <summary>
        /// Listar publicações
        /// </summary>
        /// <returns>Lista de publicações</returns>
        public async Task<List<Publication>> ListPublications()
        {
            return (await _publicationDao.ListPublications()).OrderBy(a => a.DateCreation).ToList();
        }

        /// <summary>
        /// Cadastra uma publicação
        /// </summary>
        /// <param name="publication"></param>
        /// <returns>Publicação cadastrada</returns>
        public async Task<Publication> CreatePublication(Publication publication)
        {
            publication.Id = ObjectId.GenerateNewId();
            publication.DateCreation = DateTime.Now;
            return await _publicationDao.CreatePublication(publication);
        }

        /// <summary>
        /// Busca uma publicação por id
        /// </summary>
        /// <param name="publicationId">publicação a ser buscada</param>
        public async Task<Publication> SearchPublication(ObjectId publicationId)
        {
            return await _publicationDao.SearchPublicationById(publicationId);
        }
        
        /// <summary>
        /// Reage a uma publicação
        /// </summary>
        /// <param name="userId">Usuario que reagiu</param>
        /// <param name="publicationId">publicação reagida</param>
        /// <param name="like">Reação (like = true, dislike = false)</param>
        public async Task<bool> React(ObjectId userId, ObjectId publicationId, bool like)
        {
            var publicacao = await SearchPublication(publicationId);
            if (like)
            {
                if (publicacao.Likes.Contains(userId))
                {
                    return false;
                }
                return await _publicationDao.Like(userId, publicationId);
            }
            else
            {
                if (publicacao.Dislikes.Contains(userId))
                {
                    return false;
                }
                return await _publicationDao.Dislike(userId, publicationId);
            }
        }
        

    }
}