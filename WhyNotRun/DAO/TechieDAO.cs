using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        /// Cria uma nova Techie
        /// </summary>
        /// <param name="techie"></param>
        /// <returns></returns>
        public async Task CreateTechie(Techie techie)
        {
            await Collection.InsertOneAsync(techie);
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

        public async Task<Techie> SearchTechiePerName(string name)
        {
            var filter = FilterBuilder.Eq(a => a.Name, name)
                & FilterBuilder.Exists(a => a.DeletedAt, false);
            return await Collection.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<List<Techie>> SearchTechiesPerName(string name)
        {
            var filter = FilterBuilder.Regex(a => a.Name, BsonRegularExpression.Create(new Regex(name, RegexOptions.IgnoreCase)))
                & FilterBuilder.Exists(a => a.DeletedAt, false);
            return await Collection.Find(filter).ToListAsync();
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