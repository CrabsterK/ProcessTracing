using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class TestModel
    {


        //1. Jako użytkownik, chcę mieć informację listach w danej tablicy.
        public List<string> listOfCards  = new List<string>();

        //2. Jako użytkownik, chcę mieć informację o sumie kart na danej liscie.
        public string listName;
        public int sumCardsOnList;

        //3. Jako użytkownik, chcę mieć informację o ilości przypisanych członków do dla danej tablicy.
        public string boardName;
        public int boardMembers;

        //4. Jako użytkownik, chcę mieć informację o ilości przypisanych do użytkowników kart ( procentowo/ liczbowo).
        public string cardName;
        public int cardMembers;

        //5. Jako użytkownik, chcę mieć informację o średniej ilości przypisanych na użytkownika kart.
        public Dictionary<string, int> meanOfUsersCards = new Dictionary<string, int>();

        //6. Jako użytkownik, chcę mieć informację na której liście jest najwięcej kart.
        public string largestList;

        //7. Jako użytkownik, chcę mieć informację o dacie dodania karty.
        public DateTime cardDate;
        public string cardID;

        //8. Jako użytkownik, chce mieć informację o ilości akcji wykonanych na karcie.
        public int amountofActions;

        //9. Jako użytkownik, chcę mieć informację o ilości kart znajdujących się na każdej liście.
        public Dictionary<string, int> cardsOnEachList = new Dictionary<string, int>();

        //10. Jako użytkownik, chcę mieć informację które listy są puste.
        public List<string> emptyLists = new List<string>();
    }
}