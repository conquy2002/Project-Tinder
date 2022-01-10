using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_Tinder.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserAvatarBase64String { get; set; }
        public int Age { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public bool IsAdmin { get; set; }
        public int NumberHobby { get; set; }


        
        public virtual ICollection<Interest> UserHobbies { get; set; } = new List<Interest>();

    
        public virtual ICollection<ImagerOfUser> ImagerOfUsers { get; set; } = new List<ImagerOfUser>();

      
        public virtual ICollection<NotiUser> NotiUsers { get; set; } = new List<NotiUser>();

       
        public virtual ICollection<UserConect> UserConnect { get; set; } = new List<UserConect>();
    }
}
