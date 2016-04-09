using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using ScoreKeeper.DataAccess;
using MongoDB.Driver;
using MongoDB.Bson;

namespace ScoreKeeper.Controllers
{
    public class GameController : ApiController
    {
       private readonly MongoContext context;

        public GameController()
        {
            this.context = new MongoContext();
        }

        public ICollection<Models.Game> Get()
        {
            var ret = this.context.Games
                .Find(new BsonDocument())
                .ToList();

            return ret;
        }

        public Models.Game Get(string id)
        {
            var filter = Builders<Models.Game>.Filter.Eq("Id", ObjectId.Parse(id));
            var ret = context.Games.Find(filter).FirstOrDefault();

            return ret;
        }

        public HttpResponseMessage Post(Models.Game game)
        {

            this.context.Games.InsertOne(game);

            var ret = new HttpResponseMessage(HttpStatusCode.OK) ;

            return ret;
        }

    }
}
