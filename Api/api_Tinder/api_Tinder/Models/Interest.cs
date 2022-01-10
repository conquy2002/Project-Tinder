using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace api_Tinder.Models
{
    [Table("Interest")]
    public class Interest
    {
        [Key]
        public int ID { get; set; }
        public string Hobby { get; set; }
    }
}
