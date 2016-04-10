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
        public ObjectId Id { get; set; }
        public string GameId { get; set; }
        public bool HighestScoreWins { get; set; }
        public string Description { get; set; }
        public DateTime DatePlayed { get; set; }
        public ICollection<Score> Scores { get; set; }
    }
}