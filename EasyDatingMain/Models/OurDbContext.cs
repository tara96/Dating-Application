using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace EasyDatingMain.Models
{
    public class OurDbContext : DbContext
    {
        public DbSet<UserAccount> userAccount { get; set; }

        public DbSet<Profile> Profiles { get; set; }

        public DbSet<MessageModel> Message { get; set; }

        
    }
}