using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models.UserViewModel
{
    public class AuthenticatedViewModel
    {
        public Token Token { get; set; }

        public string Name { get; set; }
        
        public string Profession { get; set; }

        public string Picture { get; set; }

    }
}