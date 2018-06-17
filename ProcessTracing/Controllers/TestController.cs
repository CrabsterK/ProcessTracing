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
        static int projectIndex = 0; // default projectIndex
        static TrelloProvider trello = new TrelloProvider(projectIndex);
        TestModel model = new TestModel();


        public void AddBoards()
        {
             List<BoardViewModel> boardsList = trello.AllBoards();
            foreach (var item in boardsList)
            {
                model.listOfBoards.Add(item.Id, item.Name);
            }
        }

        public ActionResult Index(int boardIndex = 0, int projectInd = 0)
        {
            trello = new TrelloProvider(projectInd);
            AddBoards();
            string boardID = model.listOfBoards.Keys.ElementAt(boardIndex);
            model.boardName = model.listOfBoards.Values.ElementAt(boardIndex);


            List<ListViewModel> listOfList = trello.Lists(boardID);
            List<MemberViewModel> members = trello.Members(boardID);


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

            foreach ( var item in listOfList)
            {
                model.listOfCards.Add(item.Name);
            }

            model.listsCardsQty = listOfCardAmount;

            var i = 0;
            foreach (var item in members)
            {
                i++;
            }
            model.boardMembertsQty = i;

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