using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ScoreKeeper.Controllers
{
    public class ScoreKeeperController : Controller
    {
        // GET: ScoreKeeper
        public ActionResult Index()
        {
            return View();
        }
    }
}