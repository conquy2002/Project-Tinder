using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace api_test.Models
{

    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Token { get; set; }
        public bool IsAdmin { get; set; } = false;
        public string? Name { get; set; }
        public int Age { get; set; }
        public string? City { get; set; }
        public string? Note { get; set; }
        public string? Avatar { get; set; }
        public string? Gender { get; set; }

        public virtual ICollection<Hobby> Hobbies { get; set; } = new List<Hobby>();
        public virtual ICollection<UserHobby> UserHobbys { get; set;}  = new List<UserHobby>();

        public virtual ICollection<ImagerUser> Imagers { get; set; } = new List<ImagerUser>();
        public virtual ICollection<UserContect> UserContects { get; set; } = new List<UserContect>();
        public virtual ICollection<MessgerUser> MessgerUsers { get; set; } = new List<MessgerUser>();


    }
}
