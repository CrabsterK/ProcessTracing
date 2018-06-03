using ProcessTracing.Models;
using ProcessTracing.Services;
using ProcessTracing.Services.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProcessTracing.Controllers
{
    public class TestController : Controller
    {
        TestModel model = new TestModel();
        TrelloProvider trello = new TrelloProvider();


        public void AddBoards()
        {
             List<BoardViewModel> boardsList = trello.AllBoards();
            foreach (var item in boardsList)
            {
                model.listOfBoards.Add(item.Id, item.Name);
            }
        }


       // string board = "5a93cf7b59f460b4b15b768e";
        public ActionResult Index(int boardIndex = 0)
        {
            //// 97 lista boardów danego użytkownika
            AddBoards();
            string board = model.listOfBoards.Keys.ElementAt(boardIndex);

            List<ListViewModel> listOfList = trello.Lists(board);
            List<MemberViewModel> members = trello.Members(board);
            List<CardQuantityViewMode> listOfCardAmount = new List<CardQuantityViewMode>();
            foreach (var list in listOfList)
            {
                CardQuantityViewMode cardQty = new CardQuantityViewMode();
                cardQty.ListName = list.Name;
                cardQty.CardQuantity = trello.CardsQty(list.Id);
                listOfCardAmount.Add(cardQty);
                if (cardQty.CardQuantity == 0) model.emptyLists.Add(cardQty.ListName);
            }

            List<CardViewModel> allCards = new List<CardViewModel>();
            foreach (var item in listOfList)
            {
                List<CardViewModel> listCards = trello.Cards(item.Id);
                allCards.AddRange(listCards);
            }

            List<ActionViewModel> allCardsActions = new List<ActionViewModel>();
            foreach (var card in allCards)
            {
                List<ActionViewModel> cardActions = trello.ActionsOnCard(card.ID);
                foreach(var action in cardActions)
                {
                    allCardsActions.Add(action);
                }
            }

            //1. Jako użytkownik, chcę mieć informację listach w danej tablicy.
            foreach ( var item in listOfList)
            {
                model.listOfCards.Add(item.Name);
            }

            //2. Jako użytkownik, chcę mieć informację o sumie kart na danej liscie.
            model.listsCardsQty = listOfCardAmount;

            //3. Jako użytkownik, chcę mieć informację o ilości przypisanych członków do dla danej tablicy.

            var i = 0;
            foreach (var item in members)
            {
                i++;
            }
            model.boardMembertsQty = i;


            //4. Jako użytkownik, chcę mieć informację o ilości przypisanych do użytkowników kart ( procentowo/ liczbowo).ERROR
            /*
            

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
                UsersCardsQty tmpUserQty = new UsersCardsQty();
                tmpUserQty.MemberName = memberName;
                tmpUserQty.CardQuantity = cardsQty;
                usersCardsQty.Add(tmpUserQty);
            }
            model.usersCardsQty = usersCardsQty;
            */



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
            if (listOfList.Count > 0)
            {
                var id = listOfList[0].Id;
                List<ActionViewModel> listActions = new List<ActionViewModel>();
                listActions = trello.Actions(id);
                DateTime date = new DateTime(1998, 04, 30);
                model.listCreateName = listOfList[0].Name;
                foreach (var item in listActions)
                {
                    if (item.Type.Equals("createList")) date = item.Date;
                }

                model.listCreateDate = date;
            }

            //8.Jako użytkownik, chce mieć informację o ilości akcji wykonanych na karcie w czasie.//TODO


            //10. Jako użytkownik, chcę mieć informację które listy są puste.
            //przeniesione

            //99
            Dictionary<string, int> memberAmount = new Dictionary<string, int>();
            foreach(var action in allCardsActions)
            {
                int value;
                if (memberAmount.TryGetValue(action.IdMemberCreator, out value))
                {
                    value++;
                    memberAmount[action.IdMemberCreator] = value;
                }
                else
                {
                    value = 0;
                    memberAmount.Add(action.IdMemberCreator, value);
                }
            }
            foreach(var item in memberAmount)
            {
                foreach(var member in members)
                {
                    if (member.Id == item.Key) model.amountOfCardsActions.Add(member.FullName, item.Value);
                }
            }


            //10000
            
            HashSet<int> weeks = new HashSet<int>();
            foreach (var action in allCardsActions)
            {
                DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                DateTime date1 = action.Date;
                Calendar cal = dfi.Calendar;
                weeks.Add(cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek));
            }
            List<int> sortedWeeks = weeks.ToList();
            sortedWeeks.Sort();
            model.sortedWeeks = sortedWeeks;

            List<AmountOfActionsByTime> listOfAmountOfActionsByTime = new List<AmountOfActionsByTime>();
            int amount = 0;
            foreach (var member in members)
            {
                AmountOfActionsByTime tmpObj = new AmountOfActionsByTime();
                tmpObj.member = member.FullName;
                foreach (var week in sortedWeeks) 
                {
                    amount = 0;
                        foreach(var action in allCardsActions)
                        {
                            var m=0;
                            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                            DateTime date1 = action.Date;
                            Calendar cal = dfi.Calendar;
                            m = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                            if ((m == week)&&(member.Id==action.IdMemberCreator)) amount++;
                        }  
                    tmpObj.amountOfActions.Add(amount);
                    
                }
                listOfAmountOfActionsByTime.Add(tmpObj);
            }
            model.listOfAmountOfActionsByTime = listOfAmountOfActionsByTime;

            //kto stworzyl ile kart
            List<AmountOfActionsByTime> listOfCreatedCardsByTime = new List<AmountOfActionsByTime>();
            amount = 0;
            foreach (var member in members)
            {
                AmountOfActionsByTime tmpObj = new AmountOfActionsByTime();
                tmpObj.member = member.FullName;
                foreach (var week in sortedWeeks)
                {
                    amount = 0;
                    foreach (var action in allCardsActions)
                    {
                            var m = 0;
                            DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                            DateTime date1 = action.Date;
                            Calendar cal = dfi.Calendar;
                            m = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                            if ((m == week) && (member.Id == action.IdMemberCreator)&&(action.Type.Equals("createCard"))) amount++;
                            
                    }
                    tmpObj.amountOfActions.Add(amount);

                }
                listOfCreatedCardsByTime.Add(tmpObj);
            }
            model.listOfCreatedCardsByTime = listOfCreatedCardsByTime;

            //Kto ile razy został dodany
            
            List<AmountOfActionsByTime> listOfAmountOfAddInTime = new List<AmountOfActionsByTime>();
            amount = 0;
            foreach (var member in members)
            {
                AmountOfActionsByTime tmpObj = new AmountOfActionsByTime();
                tmpObj.member = member.FullName;
                foreach (var week in sortedWeeks)
                {
                    amount = 0;
                    foreach (var action in allCardsActions)
                    {
                        var m = 0;
                        DateTimeFormatInfo dfi = DateTimeFormatInfo.CurrentInfo;
                        DateTime date1 = action.Date;
                        Calendar cal = dfi.Calendar;
                        m = cal.GetWeekOfYear(date1, dfi.CalendarWeekRule, dfi.FirstDayOfWeek);
                        if (action.Member != null)
                        {
                            if ((m == week) && (member.Id == action.Member.Id) && (action.Type.Equals("addMemberToCard"))) amount++;
                        }
                    }
                    tmpObj.amountOfActions.Add(amount);

                }
                listOfAmountOfAddInTime.Add(tmpObj);
            }
            model.listOfAmountOfAddInTime = listOfAmountOfAddInTime;

            return View(model);
        }
    }
}