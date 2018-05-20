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
        List<ListViewModel> Lists(string idBoard);
        int CardsQty(string idList);
        List<MemberViewModel> Members(string idBoard);
        string MakeRequest(string url);
        List<ActionViewModel> Actions(string idList);
    }
}
