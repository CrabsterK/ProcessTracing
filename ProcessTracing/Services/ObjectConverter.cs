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
}   