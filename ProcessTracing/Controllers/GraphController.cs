using ProcessTracing.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessTracing.Controllers
{
    public class GraphController : Controller
    {

        public ActionResult Index()
        {
            TrelloProvider trello = new TrelloProvider();
            trello.GetUserBoards();
            return View();
        }
        // GET: Graph
        public ActionResult Charts()
        {
            return View();
        }


    }
}