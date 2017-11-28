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

        public TechieBO()
        {
            _techieDao = new TechieDAO();
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

        public async Task<Techie> SearchTechiePerName(string name)
        {
            return await _techieDao.SearchTechiePerName(name);
        }

        public async Task<List<Techie>> SearchTechiesPerName(string name)
        {
            return await _techieDao.SearchTechiesPerName(name);
        }
        
        public async Task<List<Techie>> ListTechie(int page)
        {
            return (await _techieDao.ListTechies(page)).OrderBy(a => a.Name).ToList();
        }
        
        public async Task<List<Techie>> SugestTechie(string text)
        {
            return await _techieDao.SugestTechie(text);
        }

    }
}