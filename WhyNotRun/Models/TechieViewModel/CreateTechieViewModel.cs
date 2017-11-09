using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models.TechieViewModel
{
    public class CreateTechieViewModel
    {
        [Required(ErrorMessage = "O nome da Tecnologia é obrigatória")]
        public string Name { get; set; }

        public int Points { get; set; }

        public int AmountPosts { get; set; }

        public Techie ToTechie()
        {
            return new Techie
            {
                Id = ObjectId.GenerateNewId(),
                Name = Name
            };
        }
    }
}