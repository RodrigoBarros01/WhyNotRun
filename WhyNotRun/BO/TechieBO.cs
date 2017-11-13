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
    public class TechieBO
    {
        private TechieDAO _techieDao;
        private PublicationBO _publicationBo;

        public TechieBO()
        {
            _techieDao = new TechieDAO();
            _publicationBo = new PublicationBO();
        }

        public async Task<Techie> CreateTechie(Techie techie)
        {
            techie.Id = ObjectId.GenerateNewId();
            await _techieDao.CreateTechie(techie);
            return techie;
        }

        public async Task<Techie> SearchTechie(ObjectId id)
        {
            return await _techieDao.SearchTechiePerId(id);
        }

        public async Task<List<Techie>> ListTechie()
        {
            return await _techieDao.ListTechies();
        }

        public async Task<List<Techie>> ListTechieOrderByName()
        {
            var list = await _techieDao.ListTechies();
            var order = list.OrderBy(a => a.Name).ToList();
            return order;
        }

        public async Task<int> PointsPublication(ObjectId techieId)
        {
            var list = (await _publicationBo.ListPublications()).Where(a => a.Techies.Contains(techieId));
            int pontos = 0;

            foreach (var item in list)
            {
                pontos += item.Likes.Count - item.Dislikes.Count;
            }

            return pontos;
        }

        public async Task<int> AmountPostsPerTechie(ObjectId techieId)
        {
            var list = (await _publicationBo.ListPublications()).Where(a => a.Techies.Contains(techieId));
            return list.Count();
        }
    }
}