using MongoDB.Bson;
using MongoDB.Driver;
using ScoreKeeper.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ScoreKeeper.Models;
using System.Text;

namespace ScoreKeeper.Controllers
{
    public class MatchController : ApiController
    {
        private readonly MongoContext context;

        public MatchController()
        {
            this.context = new MongoContext();
        }

        public HttpResponseMessage Get()
        {
            //todo: make this return some overall stats
            var ret = (this.context.Matches.AsQueryable())
                .ToList();

            var highScoreList = (this.context.Matches.AsQueryable())
                .Where(x => x.HighestScoreWins == true)
                .Select(x => x.Scores)
                .ToList();
            var lowScoreList = (this.context.Matches.AsQueryable())
                .Where(x => x.HighestScoreWins == false)
                .Select(x => x.Scores)
                .ToList();


            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                ret);
        }

        public HttpResponseMessage Get(string id)
        {
            var ret = (this.context.Matches.AsQueryable())
                .Where(x => x.Id == ObjectId.Parse(id))
                .ToList();

            return this.Request.CreateResponse(
                HttpStatusCode.OK,
                ret);
        }

        //this is all pretty sloppy
        public HttpResponseMessage Post(Models.Match match)
        {
            var highWins = (this.context.Games.AsQueryable())
                .Where(x => x.Id == ObjectId.Parse(match.GameId))
                .Select(x => x.HighestScoreWins)
                .SingleOrDefault();

            match.HighestScoreWins = highWins;
            match.Description = GetDescription(match, highWins);

            this.context.Matches.InsertOne(match);

            //todo: replace with linq? not sure how it will affect the .PUsh
            // probably better since i need to grab something about the game up there
            var filter = Builders<Models.Game>.Filter.Eq("Id", ObjectId.Parse(match.GameId));
            var update = Builders<Models.Game>.Update
                .Push("MatchIds", match.Id);

            this.context.Games.UpdateOne(filter, update);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        private string GetDescription (Match match, bool highWins)
        {
            Score highest = null, lowest = null;

            foreach (var score in match.Scores)
            {
                if (highest == null || score.Points > highest.Points)
                {
                    highest = score;
                }
                if (lowest == null || score.Points < lowest.Points)
                {
                    lowest = score;
                }
            }

            var pattern = "{0} beat {1} {2} to {3}";
            var ret = highWins ? 
                String.Format(pattern, highest.PlayerName, lowest.PlayerName, highest.Points, lowest.Points) : 
                String.Format(pattern, lowest.PlayerName, highest.PlayerName, lowest.Points, highest.Points);

            return ret.ToString();
        }

    }
}
