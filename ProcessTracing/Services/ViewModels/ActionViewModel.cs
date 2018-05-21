using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Services.ViewModels
{
    public class ActionViewModel
    {
        public string Id { get; set; }
        public string IdMemberCreator { get; set; }
        public DataViewModel Data { get; set; }
        public string Type { get; set; }
        public DateTime Date { get; set; }
        public MemberViewModel MemberCreator { get; set; }
    }
}