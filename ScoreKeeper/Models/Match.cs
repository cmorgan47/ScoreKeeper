using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreKeeper.Models
{
    public class Match
    {
        [BsonId]
        ObjectId Id { get; set; }
        public Game Game { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}