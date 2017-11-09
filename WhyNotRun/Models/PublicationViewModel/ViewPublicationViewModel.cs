using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.BO;

namespace WhyNotRun.Models.PublicationViewModel
{
    public class ViewPublicationViewModel
    {
        public ObjectId Id { get; set; }
        
        public string Title { get; set; }
        
        public string Description { get; set; }
        
        public List<string> Techies { get; set; }
        
        public string UserName { get; set; }
        public string UserPicture { get; set; }
        public string UserProfession { get; set; }
        
        public List<Comment> Comments { get; set; }
        
        public List<ObjectId> Likes { get; set; }
        
        public List<ObjectId> Dislikes { get; set; }
        
        public DateTime DateCreation { get; set; }

        public ViewPublicationViewModel(Publication publication)
        {
            Id = publication.Id;
            Title = publication.Title;
            Description = publication.Description;

            #region Pega tecnologias

            var techieBo = new TechieBO();
            foreach (var tecId in publication.Techies)
            {
                Task.Run(async () =>
                {
                    Techies.Add((await techieBo.SearchTechiePerId(tecId)).Name);

                }).Wait();
            }

            #endregion

            #region Dados usuario

            var userBo = new UserBO();

            Task.Run(async () =>
            {
                var user = await userBo.SearchUserPerId(publication.UserId);
                UserName = user.Name;
                UserPicture = user.Picture;
                UserProfession = user.Profession;
            }).Wait();

            #endregion

            Comments = publication.Comments;
            Likes = publication.Likes;
            Dislikes = publication.Dislikes;
            DateCreation = publication.DateCreation;

        }

        public ViewPublicationViewModel()
        {

        }


        public List<ViewPublicationViewModel> ToList(List<Publication> publications)
        {
            List<ViewPublicationViewModel> publicationsList = new List<ViewPublicationViewModel>();

            foreach (var publication in publications)
            {
                publicationsList.Add(new ViewPublicationViewModel(publication));
            }

            return publicationsList;

        }




    }
}