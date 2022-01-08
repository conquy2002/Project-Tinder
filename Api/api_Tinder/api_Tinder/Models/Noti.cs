using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_Tinder.Models
{
    public class Noti
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ImageID { get; set; }
        public ImageUser Image { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int SenderID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
