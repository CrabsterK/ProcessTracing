using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class CardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int AmountOfActions { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}