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
                string memberName = member.Id;
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


            /*
            //7.Jako użytkownik, chcę mieć informację o dacie dodania karty.
            model.cardID = cardID;
            var cardDate = from card in db.CardModels
                           where card.Id == cardID
                           select card.CreatedAt;

            model.cardDate = cardDate.First();

            //8.Jako użytkownik, chce mieć informację o ilości akcji wykonanych na karcie.
            var amountOfActions = from card in db.CardModels
                           where card.Id == cardID
                           select card.AmountOfActions;

            model.amountofActions = amountOfActions.First();

            //9. Jako użytkownik, chcę mieć informację o ilości kart znajdujących się na każdej liście.
            foreach(var elem in listOfCard)
            {
                foreach (string name in numberOfCards)
                {
                    i++;
                }
                model.cardsOnEachList.Add(elem, i);
            }

            //10. Jako użytkownik, chcę mieć informację które listy są puste.
            foreach (var elem in listOfCard)
            {
                numberOfCards = from cards in db.CardModels
                                join list in db.ListModels on cards.ListId equals list.Id
                                join board in db.BoardModels on list.BoardId equals board.Id
                                join boarduser in db.UserBoardModels on board.Id equals boarduser.BoardId
                                where boarduser.UserId == UserID
                                where board.Name == boardName
                                where list.Name == elem
                                select (cards.Name);
                i = 0;
                foreach (string name in numberOfCards)
                {
                    i++;
                }
                if (i == 0) model.emptyLists.Add(elem);
            }
            */
            return View(model);
        }
    }
}