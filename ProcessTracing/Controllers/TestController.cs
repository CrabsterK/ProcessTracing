using ProcessTracing.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessTracing.Controllers
{
    public class TestController : Controller
    {
        private string UserID = "884a0319-7df0-4892-a8db-edc908ab0f17";

        private Dictionary<string, string> boards;
        private string boardName;
        private string boardID;

        private Dictionary<string, string> lists;
        private string listName;
        private string listID;

        private Dictionary<string, string> cards;
        private string cardName;
        private string cardID;

        private ApplicationDbContext db = new ApplicationDbContext();

        public ActionResult Index()
        {
            TestModel model = new TestModel();

            var boardsList = (from boards in db.BoardModels
                             join userboard in db.UserBoardModels on boards.Id equals userboard.BoardId
                             where userboard.UserId == UserID
                             select new {Name = boards.Name, ID = boards.Id });

            boardName = boardsList.First().Name;
            boardID = boardsList.First().ID;

            var listsList = from lists in db.ListModels
                            join boards in db.BoardModels on lists.BoardId equals boards.Id
                            join userboard in db.UserBoardModels on boards.Id equals userboard.BoardId
                            where userboard.UserId == UserID
                            where boards.Id == boardID
                            select new { Name = lists.Name, ID = lists.Id };
            listName = listsList.First().Name;
            listID = listsList.First().ID;


            var cardsList = from cards in db.CardModels
                            join lists in db.ListModels on cards.ListId equals lists.Id
                            join boards in db.BoardModels on lists.BoardId equals boards.Id
                            join userboard in db.UserBoardModels on boards.Id equals userboard.BoardId
                            where userboard.UserId == UserID
                            where boards.Id == boardID
                            where lists.Id == listID
                            select new { Name = cards.Name, ID = cards.Id};
            cardName = cardsList.First().Name;
            cardID = cardsList.First().ID;

            //1. Jako użytkownik, chcę mieć informację listach w danej tablicy.
            var listOfCard = from list in db.ListModels
                                join board in db.BoardModels on list.BoardId equals board.Id
                                join boarduser in db.UserBoardModels on board.Id equals boarduser.BoardId
                                where board.Name == boardName
                                where boarduser.UserId == UserID
                                select list.Name;
            foreach (string name in listOfCard)
            {
                model.listOfCards.Add(name);
            }

            //2. Jako użytkownik, chcę mieć informację o sumie kart na danej liscie.
            var numberOfCards = from cards in db.CardModels
                                join list in db.ListModels on cards.ListId equals list.Id
                                join board in db.BoardModels on list.BoardId equals board.Id
                                join boarduser in db.UserBoardModels on board.Id equals boarduser.BoardId
                                where boarduser.UserId == UserID
                                where board.Name == boardName
                                where list.Name == listName
                                select (cards.Name);
            model.listName = listName;
            var i = 0;
            foreach (string name in numberOfCards)
            {
                i++;
            }
            model.sumCardsOnList = i;

            //3. Jako użytkownik, chcę mieć informację o ilości przypisanych członków do dla danej tablicy.
            model.boardName = boardName;
            var boardMembers = from userboard in db.UserBoardModels
                               where userboard.BoardId == boardID
                               select userboard.UserId;
            i = 0;
            foreach (string name in boardMembers)
            {
                i++;
            }
            model.boardMembers = i;
            //4. Jako użytkownik, chcę mieć informację o ilości przypisanych do użytkowników kart ( procentowo/ liczbowo).
            model.cardName = cardName;
            var cardMember = from usercard in db.UserCardModels
                             join card in db.CardModels on usercard.CardId equals card.Id
                             join list in db.ListModels on card.ListId equals list.Id
                             join board in db.BoardModels on list.BoardId equals board.Id
                             where board.Id == boardID
                             where card.Name == cardName
                             select usercard.UserId;

            i = 0;
            foreach (string name in cardMember)
            {
                i++;
            }
            model.cardMembers = i;

            //5. Jako użytkownik, chcę mieć informację o średniej ilości przypisanych na użytkownika kart.
            foreach(var memberID in boardMembers)
            {
                var cards = from usercard in db.UserCardModels
                            join user in db.Users on usercard.UserId equals user.Id
                            where usercard.UserId == memberID
                            select user.Email;
                i = 0;
                for(int j = 0; j <cards.Count(); j++)
                {
                    i++;
                }
                model.meanOfUsersCards.Add(cards.First(), i);
            }

            //6. Jako użytkownik, chcę mieć informację na której liście jest najwięcej kart.
            int max = 0;
            string largestList="";
            foreach(var elem in listOfCard)
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
                if (i > max)
                {
                    max = i;
                    largestList = elem;
                }
            }
            model.largestList = largestList;

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
            return View(model);
        }
    }
}