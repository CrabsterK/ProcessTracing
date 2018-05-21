using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Services.ViewModels
{
    public class MemberViewModel
    {
        public string Id { get; set; }
        public string FullName { get; set; }
        public string UserName { get; set; }

        public override bool Equals(object obj)
        {
            var item = (MemberViewModel)obj;
            return item.Id == Id;
        }
    }
}