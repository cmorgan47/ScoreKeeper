using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ScoreKeeper.Models
{
    public class Score
    { 
        public Player Player { get; set; }
        public Int32 Points { get; set;}
    }
}