using ProcessTracing.Models;
using ProcessTracing.Services;
using ProcessTracing.Services.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessTracing.Controllers
{
    public class TestController : Controller
    {

        string board = "5a93cf7b59f460b4b15b768e";
        [Authorize]
        public ActionResult Index()
        {
            TestModel model = new TestModel();
            TrelloProvider trello = new TrelloProvider();

            List<ListViewModel> listOfList = trello.Lists(board);
            List<MemberViewModel> members = trello.Members(board);
            //1. Jako użytkownik, chcę mieć informację listach w danej tablicy.
            foreach ( var item in listOfList)
            {
                model.listOfCards.Add(item.Name);
            }

            //2. Jako użytkownik, chcę mieć informację o sumie kart na danej liscie.
            model.listsCardsQty = new List<CardQuantityViewMode>();
            foreach(var item in listOfList)
            {
                CardQuantityViewMode tmp = new CardQuantityViewMode();
                tmp.ListName = item.Name;
                tmp.CardQuantity = trello.CardsQty(item.Id);
                model.listsCardsQty.Add(tmp);
            }
            
            
            //3. Jako użytkownik, chcę mieć informację o ilości przypisanych członków do dla danej tablicy.
            
            var i = 0;
            foreach (var item in members)
            {
                i++;
            }
            model.boardMembertsQty = i;


            //4. Jako użytkownik, chcę mieć informację o ilości przypisanych do użytkowników kart ( procentowo/ liczbowo).ERROR
            List<CardViewModel> allCards = new List<CardViewModel>();
            foreach (var item in listOfList)
            {
                List<CardViewModel> listCards = trello.Cards(item.Id);
                allCards.AddRange(listCards);
            }

            List<UsersCardsQty> usersCardsQty = new List<UsersCardsQty>();
            foreach(var member in members)
            {
                string memberName = member.FullName;
                int cardsQty = 0;
                foreach(var card in allCards)
                {
                    foreach(var cardMember in card.Members)
                    {
                        if (member.Id == cardMember.Id) cardsQty++;
                    }
                }
                UsersCardsQty tmp = new UsersCardsQty();
                tmp.MemberName = memberName;
                tmp.CardQuantity = cardsQty;
                usersCardsQty.Add(tmp);
            }
            model.usersCardsQty = usersCardsQty;



            //5. Jako użytkownik, chcę mieć informację o średniej ilości przypisanych na użytkownika kart. ERROR need 4


            //6. Jako użytkownik, chcę mieć informację na której liście jest najwięcej kart.
            string maxList = "";
            int maxQty = 0;
            foreach(var item in model.listsCardsQty)
            {
                if(item.CardQuantity > maxQty)
                {
                    maxQty = item.CardQuantity;
                    maxList = item.ListName;
                }
                model.mostCardsListName = maxList;
                model.mostCardsListQty = maxQty;
            }



            //7.Jako użytkownik, chcę mieć informację o dacie dodania listy.
            var id = listOfList[0].Id;
            List<ActionViewModel> listActions = new List<ActionViewModel>();
            listActions = trello.Actions(id);
            DateTime date = new DateTime(1998, 04, 30);
            model.listCreateName = listOfList[0].Name;
            foreach(var item in listActions)
            {
                if (item.Type.Equals("createList")) date = item.Date;
            }
            
            model.listCreateDate = date;
            /*
            //8.Jako użytkownik, chce mieć informację o ilości akcji wykonanych na karcie.//TODO
            var amountOfActions = from card in db.CardModels
                           where card.Id == cardID
                           select card.AmountOfActions;

            model.amountofActions = amountOfActions.First();

            //9. Jako użytkownik, chcę mieć informację o ilości kart znajdujących się na każdej liście. //LIKE 2
            foreach(var elem in listOfCard)
            {
                foreach (string name in numberOfCards)
                {
                    i++;
                }
                model.cardsOnEachList.Add(elem, i);
            }
            */
            //10. Jako użytkownik, chcę mieć informację które listy są puste.
            model.emptyLists = new List<string>();
            foreach (var item in listOfList)
            {
                CardQuantityViewMode tmp = new CardQuantityViewMode();
                tmp.ListName = item.Name;
                tmp.CardQuantity = trello.CardsQty(item.Id);
                if(tmp.CardQuantity<1) model.emptyLists.Add(item.Name);

            }

            return View(model);
        }
    }
}