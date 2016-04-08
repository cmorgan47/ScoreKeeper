using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Driver;
using ScoreKeeper.Models;


namespace ScoreKeeper.DataAccess
{
    public class MongoContext
    {
        private readonly IMongoDatabase database;
        private readonly MongoClient client;

        public MongoContext()
        {
            var conn = ConfigurationManager.ConnectionStrings["mongoconn"].ConnectionString;
            this.client = new MongoClient(conn);
            this.database = client.GetDatabase("ScoreKeeper");
        }

        public IMongoCollection<Game> Games => database.GetCollection<Game>("Games");

        public IMongoCollection<Match> Matches => database.GetCollection<Match>("Matches"); 

        public IMongoDatabase Db
        {
            get
            {
                return this.database;
            }
        }
    }
}