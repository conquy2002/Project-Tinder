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
        public DbSet<api_Tinder.Models.ImagerOfUser> ImagerOfUser { get; set; }
        public DbSet<api_Tinder.Models.Interest> Interest { get; set; }
        public DbSet<api_Tinder.Models.NotiUser> NotiUser { get; set; }
        public DbSet<api_Tinder.Models.MessgerUser> MessgerUser { get; set; }
        public DbSet<api_Tinder.Models.UserHobby> UserHobby { get; set; }
        public DbSet<api_Tinder.Models.UserConect> UserConect { get; set; }
        public DbSet<api_Tinder.Models.LoginViewModel> LoginViewModel { get; set; }
        public DbSet<api_Tinder.Models.RegistrationModel> RegistrationModel { get; set; }
        public DbSet<api_Tinder.Models.logintoken> logintoken { get; set; }


    }
}
