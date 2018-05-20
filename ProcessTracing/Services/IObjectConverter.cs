using ProcessTracing.Models;
using ProcessTracing.Services.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProcessTracing.Services
{
    interface IListObjectConverter
    {
        List<ListViewModel> ConvertList(string response);
    }
    interface IQuantityObjectCOnverter
    {
        int ConvertQuantity(string response);
    }
    interface IMembersConverter
    {
        List<MemberViewModel> ConvertMembers(string response);
    }
    interface IActionConverter
    {
        List<ActionViewModel> ConvertAction(string response);
    }
    interface ICardsConverter
    {
        List<CardViewModel> ConvertCards(string response);
    }
}
