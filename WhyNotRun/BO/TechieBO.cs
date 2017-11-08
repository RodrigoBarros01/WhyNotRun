using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WhyNotRun.DAO;

namespace WhyNotRun.BO
{
    public class TechieBO
    {
        private TechieDAO _techiaDao;

        public TechieBO()
        {
            _techiaDao = new TechieDAO();
        }



    }
}