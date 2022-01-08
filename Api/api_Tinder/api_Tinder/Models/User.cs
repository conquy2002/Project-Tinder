using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_Tinder.Models
{
    public class User
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string UserAvatarBase64String { get; set; }
    }
}
