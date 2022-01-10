using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_Tinder.Models
{
    public class ImagerOfUser
    {
        [Key]
        public int ID { get; set; }
        public string UserImgBase64String { get; set; }
        public int UserId { get; set; }
        public string Content { get; set; }
    }
}
