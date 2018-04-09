using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class BoardModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UpdatedAt { get; set; }
        public string Owner { get; set; }
    }
}