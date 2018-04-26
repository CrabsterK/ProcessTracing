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
}