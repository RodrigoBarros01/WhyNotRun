using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WhyNotRun.Models
{
    public class Comment
    {
        [BsonElement("_id")]
        public ObjectId Id { get; set; }

        [BsonElement("user")]
        public User User { get; set; }

        [BsonElement("publication")]
        public ObjectId Publication { get; set; }

        [BsonElement("description")]
        public string Description { get; set; }
        
        [BsonElement("dateCreation")]
        public DateTime DateCreation { get; set; }

        [BsonElement("deletedAt"), BsonIgnoreIfNull]
        public DateTime? DeletedAt { get; set; }
        
    }
}