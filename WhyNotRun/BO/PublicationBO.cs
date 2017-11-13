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
        private UserBO _userBo;

        public PublicationBO()
        {
            _publicationDao = new PublicationDAO();
            _userBo = new UserBO();
        }

        /// <summary>
        /// Listar publicações
        /// </summary>
        /// <returns>Lista de publicações</returns>
        public async Task<List<Publication>> ListPublications()
        {
            return await _publicationDao.ListPublications();
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

            await _publicationDao.CreatePublication(publication);
            return publication;
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

        /// <summary>
        /// Adiciona um comentario a uma publicação
        /// </summary>
        /// <param name="comment">Comentario a ser adicionado</param>
        /// <param name="publicationId">publicação que vai receber o comentario</param>
        public async Task<bool> AddComment(Comment comment, ObjectId publicationId)
        {
            comment.Id = ObjectId.GenerateNewId();
            comment.DateCreation = DateTime.Now;
            
            var user = await _userBo.SearchUserPerId(comment.UserId);
            comment.UserName = user.Name;
            comment.UserPicture = user.Picture;
            comment.UserProfession = user.Profession;

            return await _publicationDao.AddComment(comment, publicationId);
        }
        
        //rever esse metodo
        public async Task<List<Publication>> SearchPublications(string textToSearch)
        {
            var publications = (await ListPublications()).Where(a => a.Title.Contains(textToSearch) || a.Description.Contains(textToSearch)).ToList();
            
            return publications;
        }


    }
}