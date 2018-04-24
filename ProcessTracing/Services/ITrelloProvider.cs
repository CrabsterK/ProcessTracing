using ProcessTracing.Models;
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
        List<ListModel> GetListsRelatedToBoard(int id);
        string MakeRequest(string url);
    }
}
