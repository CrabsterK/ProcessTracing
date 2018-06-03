using ProcessTracing.Services;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections;
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
            TrelloProvider trello = new TrelloProvider(0);
            //tu jest zwrocona lista list dla danej tablicy , na razie nie jest zwracana do widoku -
            //Przypadku uzycia nr 1
            List<ListViewModel> list = trello.Lists(board);

            //przypadek użycia nr 2
            List<CardQuantityViewMode> cardQty = new List<CardQuantityViewMode>();
            foreach(var item in list)
            {
                var qty = trello.CardsQty(item.Id);
                cardQty.Add(new CardQuantityViewMode
                {
                    ListName=item.Name,
                    CardQuantity=qty
                });
            }

            //przypadek uzycia nr 3, 6 i 10
            List<MemberViewModel> members = trello.Members(board);

            var check = 9;


            //Clean all database
            List<ListViewModel> list1 = trello.Lists(board);


            List<ActionViewModel> actions = trello.Actions(list.FirstOrDefault().Id);


            List<CardViewModel> cards = trello.Cards(list.FirstOrDefault().Id);
            foreach (var item in cards)
            {
                List<MemberViewModel> cardMembers = trello.CardMembers(item.ID);
                item.Members = cardMembers;
            }

            foreach (var item in cards)
            {
                List<ActionViewModel> actionsOnCard = trello.ActionsOnCard(item.ID);
            }


            //ilosc kart dla danego uzytkownika - tu bierze tylko dla kart z pierwszej listy
            Hashtable assignCardsToMember = new Hashtable();
            foreach (var item in members)
            {
                assignCardsToMember.Add(item.Id, 0);
            }
            foreach (var item in cards)
            {
                foreach (var t in item.Members)
                {
                    var value = (int)assignCardsToMember[t.Id];
                    value++;
                    assignCardsToMember[t.Id] = value;
                }
            }


            return View();
        }
        // GET: Graph
        public ActionResult Charts()
        {
            return View();
        }

        //		Id	"5a4e08009adbd336a4569daa"	string


    }
}