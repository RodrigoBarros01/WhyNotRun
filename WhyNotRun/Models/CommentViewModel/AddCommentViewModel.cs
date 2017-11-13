using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using WhyNotRun.BO;

namespace WhyNotRun.Models.CommentViewModel
{
    public class AddCommentViewModel
    {
        [Required(ErrorMessage = "O usuário é obrigatório")]
        public string UserId { get; set; }

        [Required(ErrorMessage = "A descriação é obrigatória")]
        public string Description { get; set; }

        [Required(ErrorMessage = "A publicação é obrigatória")]
        public string PublicationId { get; set; }
        public Comment ToComment()
        {
            return new Comment
            {
                UserId = UserId.ToObjectId(),
                Description = Description
            };
        }
    }
}