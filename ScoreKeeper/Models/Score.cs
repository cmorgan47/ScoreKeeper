using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreKeeper.Models
{
    public class Score
    { 
        //replace this with a player ObjectId when the player object gets bigger
        public string PlayerName { get; set; }
        public Int32 Points { get; set;}
    }
}