using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class CheckItemModel
    {
        [Key]
        public string Id { get; set; }

        [Required]
        public string ChecklistId { get; set; }

        [ForeignKey(nameof(ChecklistId))]
        public ChecklistModel Checklist { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string State { get; set; }
    }
}