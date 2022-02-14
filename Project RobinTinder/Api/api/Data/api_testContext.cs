#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using api_test.Models;

namespace api_test.Data
{
    public class api_testContext : DbContext
    {
        public api_testContext (DbContextOptions<api_testContext> options)
            : base(options)
        {
        }

        public DbSet<api_test.Models.Hobby> Hobby { get; set; }
        public DbSet<api_test.Models.NotiUser> NotiUser { get; set; }
        public DbSet<api_test.Models.UserHobby> UserHobby { get; set; }
        public DbSet<api_test.Models.User> User { get; set; }
        public DbSet<api_test.Models.ImagerUser> ImagerUser { get; set; }
        public DbSet<api_test.Models.UserContect> UserContect { get; set; }
        public DbSet<api_test.Models.MessgerUser> MessgerUser { get; set; }
        public DbSet<api_test.Models.MessgerBody> MessgerBody { get; set; }
        public DbSet<api_test.Models.ImagerMessger> ImagerMessger { get; set; }

    }
}
