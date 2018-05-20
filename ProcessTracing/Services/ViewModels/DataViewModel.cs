using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Services.ViewModels
{
    public class DataViewModel
    {
        public BoardViewModel Board { get; set; }
        public ListViewModel List { get; set; }
        public CardViewModel Card { get; set; }
    }
}