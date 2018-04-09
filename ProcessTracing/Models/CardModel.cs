using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class CardModel
    {
        [Key]
        public string Id { get; set; }

        public string ListId { get; set; }

        [ForeignKey(nameof(ListId))]
        public ListModel List { get; set;}

        public string Name { get; set; }

        public string Description { get; set; }

        public int AmountOfActions { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}