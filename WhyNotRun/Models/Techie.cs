using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models
{
    public class Techie
    {
        public Techie()
        {
            Points = 0;
            Posts = 0;
        }


        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("name")]
        public string Name { get; set; }

        [BsonElement("points")]
        public int Points { get; set; }

        [BsonElement("posts")]
        public int Posts { get; set; }


        [BsonElement("deletedAt"), BsonIgnoreIfNull]
        public DateTime DeletedAt { get; set; }
    }
}