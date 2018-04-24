using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using ProcessTracing.Models;

namespace ProcessTracing.Services
{
    public class ListObjectConverter : IListObjectConverter
    {
        public List<ListModel> ConvertList(string response)
        {
            JavaScriptSerializer js = new JavaScriptSerializer();
            var obj = js.Deserialize<dynamic>(response);
            ListModel list = new ListModel();
            return null;
        }
    }
}