using ProcessTracing.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTracing.Services
{
    interface ITrelloProvider
    {
        string Url(string s1);
        void GetUserBoards();
        List<List> Lists(string idBoard);
        int CardsQty(string idList);
        List<Member> Members(string idBoard);
        string MakeRequest(string url);
    }
}
