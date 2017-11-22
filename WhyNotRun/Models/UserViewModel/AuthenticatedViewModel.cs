﻿using MongoDB.Bson;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models.UserViewModel
{
    public class AuthenticatedViewModel
    {
        [JsonProperty(PropertyName = "token")]
        public string Token { get; set; }

        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }

        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }

        [JsonProperty(PropertyName = "profession")]
        public string Profession { get; set; }

        [JsonProperty(PropertyName = "picture")]
        public string Picture { get; set; }

        public AuthenticatedViewModel(User user)
        {
            Token token = new Token();
            Token = token.GenerateToken(user.Id.ToString());
            Id = user.Id.ToString();
            Name = user.Name;
            Profession = user.Profession;
            Picture = user.Picture;
        }

    }
}