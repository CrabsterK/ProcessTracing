using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ProcessTracing.Models;
using ProcessTracing.Services.ViewModels;

namespace ProcessTracing.Services
{
    public class ListObjectConverter : IListObjectConverter
    {
        public List<ListViewModel> ConvertList(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ListViewModel[] array = new JavaScriptSerializer().Deserialize<ListViewModel[]>(response);
            return array.ToList();
        }
    }

    public class QuantityObjectConverter : IQuantityObjectCOnverter
    {
        public int ConvertQuantity(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            Object[] list = new JavaScriptSerializer().Deserialize<Object[]>(response);
            return list.Length;
        }
    }
    public class MembersConverter : IMembersConverter
    {
        public List<MemberViewModel> ConvertMembers(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            MemberViewModel[] members = new JavaScriptSerializer().Deserialize<MemberViewModel[]>(response);
            return members.ToList();
        }
    }

    public class BoardsConverter : IBoardsConverter
    {
        public List<BoardViewModel> ConvertBoards(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            BoardViewModel[] boards = new JavaScriptSerializer().Deserialize<BoardViewModel[]>(response);
            return boards.ToList();
        }
    }

    public class ActionConverter : IActionConverter
    {
        public List<ActionViewModel> ConvertAction(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            ActionViewModel[] actions = new JavaScriptSerializer().Deserialize<ActionViewModel[]>(response);
            return actions.ToList();
        }
    }
    public class CardsConverter : ICardsConverter
    {
        public List<CardViewModel> ConvertCards(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            CardViewModel[] cards = new JavaScriptSerializer().Deserialize<CardViewModel[]>(response);
            return cards.ToList();
        }
    }
}