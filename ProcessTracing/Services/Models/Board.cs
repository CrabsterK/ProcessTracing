using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Services.Models
{
    public class Board
    {
        List<List> List { get; set; }
        List<Member> Members{get; set;}
    }
}