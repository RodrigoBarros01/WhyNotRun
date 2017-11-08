using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models.UserViewModel
{
    public class VisualizationUserViewModel
    {
        public VisualizationUserViewModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Email = user.Email;
            Password = user.Password;
            Profession = user.Profession;
            Picture = user.Picture;
        }

        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Profession { get; set; }
        public string Picture { get; set; }
    }
}