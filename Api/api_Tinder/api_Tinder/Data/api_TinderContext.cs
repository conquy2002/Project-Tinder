#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_Tinder.Models;

namespace api_Tinder.Data
{
    public class api_TinderContext : DbContext
    {
        public api_TinderContext (DbContextOptions<api_TinderContext> options)
            : base(options)
        {
        }

        public DbSet<api_Tinder.Models.User> User { get; set; }

        public DbSet<api_Tinder.Models.ImageUser> ImageUser { get; set; }
        public DbSet<api_Tinder.Models.Messger> Messgers { get; set; }
        public DbSet<api_Tinder.Models.MessgerId> MessgerIds { get; set; }
        public DbSet<api_Tinder.Models.Post> Posts { get; set; }
        public DbSet<api_Tinder.Models.UserConnect> UserConnects { get; set; }
        public DbSet<api_Tinder.Models.Interest> Interests { get; set; }


    }
}
