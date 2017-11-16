using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using WhyNotRun.BO;
using WhyNotRun.Models.TechieViewModel;

namespace WhyNotRun.Models.PublicationViewModel
{
    public class ViewPublicationViewModel
    {
        [JsonProperty(PropertyName = "id")]
        public ObjectId Id { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "description")]
        public string Description { get; set; }
        
        [JsonProperty(PropertyName = "techies")]
        public List<ViewTechieViewModel> Techies { get; set; }
        //public List<string> Techies { get; set; }

        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
        [JsonProperty(PropertyName = "userpicture")]
        public string UserPicture { get; set; }
        [JsonProperty(PropertyName = "userprofession")]
        public string UserProfession { get; set; }

        [JsonProperty(PropertyName = "comments")]
        public List<Comment> Comments { get; set; }

        [JsonProperty(PropertyName = "points")]
        public int Points { get; set; }

        [JsonProperty(PropertyName = "datecreation")]
        public DateTime DateCreation { get; set; }

        public ViewPublicationViewModel(Publication publication)
        {
            Id = publication.Id;
            Title = publication.Title;
            Description = publication.Description;

            Points = publication.Likes.Count() - publication.Dislikes.Count();


            #region Pega tecnologias

            if (publication.Techies.Count > 0)
            {
                this.Techies = new List<ViewTechieViewModel>();
                List<Task<Techie>> tasks = new List<Task<Techie>>();

                var techieBo = new TechieBO();
                foreach (var tecId in publication.Techies)
                    tasks.Add(Task.Run(() => techieBo.SearchTechie(tecId)));

                Task.WaitAll(tasks.ToArray());
                Parallel.ForEach(tasks, task => Techies.Add(new ViewTechieViewModel(task.Result)));
            }

            #endregion

            #region Dados usuario

            var userBo = new UserBO();

            Task.Run(async () =>
            {
                var user = await userBo.SearchUserPerId(publication.UserId);
                if (user != null)
                {
                    UserName = user.Name;
                    UserPicture = user.Picture;
                    UserProfession = user.Profession;
                }
            }).Wait();

            #endregion

            Comments = publication.Comments;

            DateCreation = publication.DateCreation;

        }

        public ViewPublicationViewModel()
        {

        }


        public static List<ViewPublicationViewModel> ToList(List<Publication> publications)
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