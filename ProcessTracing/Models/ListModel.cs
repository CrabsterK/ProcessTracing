using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class ListModel
    {
        [Key]
        public string Id { get; set; }

        public string BoardId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public BoardModel Board { get; set; }

        public string Name { get; set; }
    }
}