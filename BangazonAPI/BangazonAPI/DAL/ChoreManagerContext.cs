using BangazonAPI.Models;
using BangazonAPI.Providers.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace BangazonAPI.DAL
{
    public class ChoreManagerContext: ApplicationDbContext
    {
        public virtual DbSet<Chore> Chores { get; set; }
    }
}