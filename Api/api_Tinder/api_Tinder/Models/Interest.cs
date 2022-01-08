namespace api_Tinder.Models
{
    public class Interest
    {
        public string Id { get; set; }
        public string Hobby { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
