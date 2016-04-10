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

        public HttpResponseMessage Get()
        {
            var ret= (this.context.Games.AsQueryable())
                .Select(x => new
                {
                    x.Id, 
                    x.Name
                })
                .ToList();

            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                ret);
        }

        public HttpResponseMessage Get(string id)
        {
            var ret = (this.context.Games.AsQueryable())
                .Where(x => x.Id == ObjectId.Parse(id))                
                .SingleOrDefault();

            ret.Matches = (this.context.Matches.AsQueryable())
                .Where(x => x.GameId == id)
                .ToList();

            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                ret);
        }

        public HttpResponseMessage Post(Models.Game game)
        {
            game.MatchIds = new List<ObjectId>();
            this.context.Games.InsertOne(game);

            var ret = new HttpResponseMessage(HttpStatusCode.OK) ;

            return ret;
        }

    }
}
