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
        List<ListViewModel> GetListsRelatedToBoard(string id);
        int GetCardsQtyFromList(string id);
        List<MemberViewModel> GetMembers(string id);
        string MakeRequest(string url);
    }
}
