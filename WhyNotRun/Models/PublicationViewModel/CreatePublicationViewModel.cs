using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.BO;

namespace WhyNotRun.Models.PublicationViewModel
{
    public class CreatePublicationViewModel
    {
        [Required(ErrorMessage ="O título é obrigatório")]
        public string Title { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória")]
        public string Description { get; set; }

        //[Required(ErrorMessage = "As tecnologias são obrigatórios")]
        public List<string> Techies { get; set; }

        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string User { get; set; }

        public Publication ToPublication()
        {
            List<ObjectId> techiesId = new List<ObjectId>();
            foreach (var id in Techies)
            {
                techiesId.Add(id.ToObjectId());
            }
            
            return new Publication
            {
                Title = Title,
                Description = Description,
                Techies = techiesId,
                UserId = User.ToObjectId()
            };
        }

    }
}