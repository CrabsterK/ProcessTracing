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
        //tu jest na razie syf ale to do poprawy, na razie wszystkie obiekty bede zwracac tuuuuu
        string board = "5a93cf7b59f460b4b15b768e";
        public ActionResult Index()
        {
            TrelloProvider trello = new TrelloProvider();
            //tu jest zwrocona lista list dla danej tablicy , na razie nie jest zwracana do widoku -
            //Przypadku uzycia nr 1
            List<ListViewModel> list = trello.GetListsRelatedToBoard(board);

            //przypadek użycia nr 2
            List<CardQuantityViewMode> cardQty = new List<CardQuantityViewMode>();
            foreach(var item in list)
            {
                var qty = trello.GetCardsQtyFromList(item.Id);
                cardQty.Add(new CardQuantityViewMode
                {
                    ListName=item.Name,
                    CardQuantity=qty
                });
            }
            //przypadek uzycia nr 3 
            List<MemberViewModel> members = trello.GetMembers(board);
            var check = 9;
            return View();
        }
        // GET: Graph
        public ActionResult Charts()
        {
            return View();
        }

      

    }
}