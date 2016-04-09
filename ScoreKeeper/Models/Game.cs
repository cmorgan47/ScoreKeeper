using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreKeeper.Models
{
    public class Game
    {
        [BsonId]
        public ObjectId Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> Categories { get; set; }
        public bool HighestScoreWins { get; set; }
        public ICollection<ObjectId> MatchIds { get; set; }
    }
}