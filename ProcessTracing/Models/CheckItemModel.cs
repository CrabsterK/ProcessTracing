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

        public string ChecklistId { get; set; }

        [ForeignKey(nameof(ChecklistId))]
        public ChecklistModel Checklist { get; set; }

        public string Name { get; set; }

        public string State { get; set; }
    }
}