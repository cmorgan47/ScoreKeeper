using MongoDB.Bson;
using MongoDB.Driver;
using ScoreKeeper.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ScoreKeeper.Controllers
{
    public class MatchController : ApiController
    {
        private readonly MongoContext context;

        public MatchController()
        {
            this.context = new MongoContext();
        }

        public ICollection<Models.Match> Get()
        {
            return this.context.Matches.Find(new BsonDocument()).ToList();
        }

        public Models.Match Get(string id)
        {
            var filter = Builders<Models.Match>.Filter.Eq("Id", ObjectId.Parse(id));
            var ret = this.context.Matches.Find(filter).SingleOrDefault();
            return ret;
        }

        public HttpResponseMessage Post(Models.Match match)
        {
            var filter = Builders<Models.Game>.Filter.Eq("Id", ObjectId.Parse(match.GameId));
            
            this.context.Matches.InsertOne(match);

            var update = Builders<Models.Game>.Update
                .Push("MatchIds", match.Id);
            
            this.context.Games.UpdateOne(filter, update);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
