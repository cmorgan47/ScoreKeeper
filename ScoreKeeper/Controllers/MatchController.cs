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

        public HttpResponseMessage Post(Models.Match match)
        {
            this.context.Matches.InsertOne(match);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}
