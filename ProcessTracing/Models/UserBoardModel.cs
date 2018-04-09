using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class UserBoardModel
    {
        [Key, Column(Order = 0)]
        public string BoardId { get; set; }
        [Key, Column(Order = 1)]
        public string UserId { get; set; }

        [ForeignKey(nameof(BoardId))]
        public BoardModel Board { get; set; }

        [ForeignKey(nameof(UserId))]
        public ApplicationUser User { get; set; }

        public string MemberType { get; set; }
    }
}