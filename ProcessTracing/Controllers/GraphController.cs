using ProcessTracing.Services;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessTracing.Controllers
{
    public class GraphController : Controller
    {
        //TODO id boardu przekazane w parametrze to metody Index, na razie na sztywno
        string board = "5a93cf7b59f460b4b15b768e";
        public ActionResult Index()
        {
            TrelloProvider trello = new TrelloProvider();
            //tu jest zwrocona lista list dla danej tablicy , na razie nie jest zwracana do widoku - jak roić partial view
            List<ListViewModel> list = trello.GetListsRelatedToBoard(board);

            return View();
        }
        // GET: Graph
        public ActionResult Charts()
        {
            return View();
        }


    }
}