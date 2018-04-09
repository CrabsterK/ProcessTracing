using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class BoardModel
    {
        [Key]
        public string Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime UpdatedAt { get; set; }

        public string Owner { get; set; }
    }
}