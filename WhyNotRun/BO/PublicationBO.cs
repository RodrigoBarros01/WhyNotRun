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

        public async Task<Publication> CreatePublication(Publication publication)
        {
            publication.Id = ObjectId.GenerateNewId();




        }



    }
}