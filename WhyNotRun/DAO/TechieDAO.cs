using MongoDB.Bson;
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
    }
}