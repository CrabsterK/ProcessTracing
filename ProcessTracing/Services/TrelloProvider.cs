using ProcessTracing.Models;
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

        //tymczasowe przechowyanie
        string key = "f27de49532bf871ca048feeb9e3ea8dc";
        string token = "442ec0924e45b0b9b1453aecc9007cbd47fec9fb873a16be2cdcc45e7a82122f";

        public TrelloProvider()
        {
            
        }

        public void GetUserBoards()
        {

        }

        public List<ListViewModel> Lists(string id)
        {
            string url = Url(String.Format("boards/{0}/lists", id));
            var data = MakeRequest(url);
            ListObjectConverter converter = new ListObjectConverter();
            return converter.ConvertList(data);
        }
        public int CardsQty(string id)
        {
            string url = Url(String.Format("lists/{0}/cards", id));
            var data = MakeRequest(url);
            QuantityObjectConverter converter = new QuantityObjectConverter();
            return converter.ConvertQuantity(data);
        }
        public List<MemberViewModel> Members(string id)
        {
            string url=Url(String.Format("boards/{0}/members", id));
            var data = MakeRequest(url);
            MembersConverter converter = new MembersConverter();
            return converter.ConvertMembers(data);
        }

        public List<ActionViewModel> Actions(string idList)
        {
            string url = Url(String.Format("lists/{0}/actions", idList));
            var data = MakeRequest(url);
            ActionConverter converter = new ActionConverter();
            return converter.ConvertAction(data);
        }

        public string MakeRequest(string url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
            request.MediaType = "GET";
            HttpWebResponse respone = request.GetResponse() as HttpWebResponse;
            var dataStream = respone.GetResponseStream();
            StreamReader reader = new StreamReader(dataStream);
            return reader.ReadToEnd();

        }

        public string Url(string s1)
        {
            return String.Format("https://api.trello.com/1/{0}?key={1}&token={2}", s1, key, token);            
        }

      
    }
}