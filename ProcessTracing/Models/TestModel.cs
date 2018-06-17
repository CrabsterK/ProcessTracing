using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class TestModel
    {
        public string boardName;

        public List<string> listOfCards  = new List<string>();

        public List<CardQuantityViewMode> listsCardsQty;

        public int boardMembertsQty;

        public Dictionary<string, int> meanOfUsersCards = new Dictionary<string, int>();

        public string mostCardsListName;

        public int mostCardsListQty;

        public DateTime listCreateDate;

        public string listCreateName;

        public List<string> emptyLists = new List<string>();

        public Dictionary<string, int> amountOfCardsActions = new Dictionary<string, int>();

        public List<AmountOfActionsByTime> listOfAmountOfActionsByTime = new List<AmountOfActionsByTime>();

        public List<int> sortedWeeks = new List<int>();

        public Dictionary<string, string> listOfBoards = new Dictionary<string, string>();

        public List<AmountOfActionsByTime> listOfCreatedCardsByTime = new List<AmountOfActionsByTime>();

        public List<AmountOfActionsByTime> listOfAmountOfAddInTime = new List<AmountOfActionsByTime>();
    }
}