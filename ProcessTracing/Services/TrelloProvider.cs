﻿using ProcessTracing.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;

namespace ProcessTracing.Services
{
    public class TrelloProvider : ITrelloProvider
    {
        HttpWebRequest request;
        string key;
        string token;

        public TrelloProvider(int projectIndex)
        {
            if (projectIndex == 0)
            {
                //Process Tracing
                key = "0a9c02bbe066e66428ef7d8689020999";
                token = "8b04a1fd25679ff3ecd2335a1b519e9dd6830ec542e0f970b46b84ac8efa6830";
            }
            if (projectIndex == 1)
            {
                //Grupa Sekretarz
                key = "4a77263de1a63c913967e143caf868dd";
                token = "e839b17d42063c3ad2ab85290bc5eeebe9b448751bb0806df96574b927b9dd83";
            }
            if (projectIndex == 2)
            {
                //Grupa Drony
                key = "f66823405f06612b2b89aa8d08bd7303";
                token = "b5b1f181546949cf513f99f38d5c712c27ad3ec8cc7a98824c5b4e16981c127d";
            }
            if (projectIndex == 3)
            {
                //Grupa Komiks
                key = "92971f42e3f6f7331ad38950971e3092";
                token = "d803fe35b527def9e29fb86060f5100ddc7a16f3185ed6857b3084baa9436d50";
            }
        }

        public void GetUserBoards()
        {

        }

        public List<ListViewModel> Lists(string id)
        {
            string url = Url(String.Format("boards/{0}/lists?", id));
            var data = MakeRequest(url);
            ListObjectConverter converter = new ListObjectConverter();
            return converter.ConvertList(data);
        }
        public int CardsQty(string id)
        {
            string url = Url(String.Format("lists/{0}/cards?", id));
            var data = MakeRequest(url);
            QuantityObjectConverter converter = new QuantityObjectConverter();
            return converter.ConvertQuantity(data);
        }
        public List<MemberViewModel> Members(string id)
        {
            string url=Url(String.Format("boards/{0}/members?", id));
            var data = MakeRequest(url);
            MembersConverter converter = new MembersConverter();
            return converter.ConvertMembers(data);
        }

        public List<ActionViewModel> Actions(string idList)
        {
            string url = Url(String.Format("lists/{0}/actions?", idList));
            var data = MakeRequest(url);
            ActionConverter converter = new ActionConverter();
            return converter.ConvertAction(data);
        }

        public List<ActionViewModel> ActionsOnCard(string idCard)
        {
            string url = Url(String.Format("cards/{0}/actions?filter=all&", idCard));
            var data = MakeRequest(url);
            ActionConverter converter = new ActionConverter();
            return converter.ConvertAction(data);
        }

        public List<BoardViewModel> AllBoards()
        {
            string url = Url(String.Format("/member/me/boards?"));
            var data = MakeRequest(url);
            BoardsConverter converter = new BoardsConverter();
            return converter.ConvertBoards(data);
        }

        public List<MemberViewModel> CardMembers(string idCard)
        {
            string url = Url(String.Format("cards/{0}/members?", idCard));
            var data = MakeRequest(url);
            MembersConverter converter = new MembersConverter();
            return converter.ConvertMembers(data);
        }
        public List<CardViewModel> Cards(string idList)
        {
            string url = Url(String.Format("lists/{0}/cards?", idList));
            var data = MakeRequest(url);
            CardsConverter converter = new CardsConverter();
            return converter.ConvertCards(data);
        }
        public string MakeRequest(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.MediaType = "GET";
            HttpWebResponse respone = request.GetResponse() as HttpWebResponse;

            var dataStream =  respone.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();

        }

        public string Url(string s1)
        {
            return String.Format("https://api.trello.com/1/{0}key={1}&token={2}", s1, key, token);            
        }

        //            return String.Format("https://api.trello.com/1/{0}?key={1}&token={2}", s1, key, token);            

    }
}