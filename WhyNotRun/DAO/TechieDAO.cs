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
    public class TechieDAO : ContextAsyncDAO<Techie>
    {
        public TechieDAO() : base()
        {

        }

        /// <summary>
        /// Faz a busca de uma Techie pelo Id
        /// </summary>
        /// <param name="id"> Id da Techie</param>
        /// <returns> retorna uma Techie </returns>
        public async Task<Techie> SearchTechiePerId(ObjectId id)
        {
            var filter = FilterBuilder.Eq(a => a.Id, id)
                & FilterBuilder.Exists(a => a.DeletedAt, false);
            var result = await Collection.Find(filter).FirstOrDefaultAsync();
            return result;
        }

        /// <summary>
        /// Lista as Techies não deletadas
        /// </summary>
        /// <returns> Retorna uma lista de Techies</returns>
        public async Task<List<Techie>> ListTechies()
        {
            var filter = FilterBuilder.Exists(a => a.DeletedAt, false);
            var result = await Collection.Find(filter).ToListAsync();
            return result;
        }
    }
}