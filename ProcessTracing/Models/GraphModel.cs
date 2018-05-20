using ProcessTracing.Services.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class GraphModel
    {
        //1
        List<ListViewModel> list;

        //2
        List<CardQuantityViewMode> cardQty;

        //3
        List<MemberViewModel> members;

        //Akcje z listy
        List<ActionViewModel> actions;
    }
}