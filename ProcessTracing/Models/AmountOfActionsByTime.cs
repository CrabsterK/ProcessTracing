using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class AmountOfActionsByTime
    {
        public string member;
        public List<int> amountOfActions = new List<int>();
    }
}