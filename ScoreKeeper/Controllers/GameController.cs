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
            
            return this.context.Games
                .Find(new BsonDocument())
                .ToList();
        }

        public HttpResponseMessage Post(Models.Game game)
        {

            this.context.Games.InsertOne(game);

            var ret = new HttpResponseMessage(HttpStatusCode.OK) ;

            return ret;
        }

    }
}
