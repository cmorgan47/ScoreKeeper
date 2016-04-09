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

        // cmorgan: leaving commented code in this version to illustrate the difference

        public HttpResponseMessage Get()
        {
            //var ret = this.context.Games
            //    .Find(new BsonDocument())
            //    .ToList();

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
            //var filter = Builders<Models.Game>.Filter.Eq("Id", ObjectId.Parse(id));
            //var ret = context.Games.Find(filter).FirstOrDefault();

            var ret = (this.context.Games.AsQueryable())
                .Select(x => x)
                .Where(x => x.Id == ObjectId.Parse(id))
                .SingleOrDefault();

            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                ret);
        }

        public HttpResponseMessage Post(Models.Game game)
        {

            this.context.Games.InsertOne(game);

            var ret = new HttpResponseMessage(HttpStatusCode.OK) ;

            return ret;
        }

    }
}
