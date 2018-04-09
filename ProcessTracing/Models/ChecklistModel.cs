using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class ChecklistModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string CardId { get; set; }

        [ForeignKey(nameof(CardId))]
        public CardModel Card { get; set; }

        [Required]
        public string Name { get; set; }
    }
}