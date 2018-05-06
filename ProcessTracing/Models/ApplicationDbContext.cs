using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ProcessTracing.Models
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<BoardModel> BoardModels { get; set; }
        public DbSet<CardModel> CardModels { get; set; }
        public DbSet<CheckItemModel> CheckItemModels { get; set; }
        public DbSet<ChecklistModel> ChecklistModels { get; set; }
        public DbSet<ListModel> ListModels { get; set; }
        public DbSet<UserBoardModel> UserBoardModels { get; set; }
        public DbSet<UserCardModel> UserCardModels { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}