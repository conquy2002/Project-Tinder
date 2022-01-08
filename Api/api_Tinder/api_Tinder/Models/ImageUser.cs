namespace api_Tinder.Models
{
    public class ImageUser
    {
        public string Id { get; set; }
        public string ImageBase64String { get; set; }
        public string UserID { get; set; }
        public User User { get; set; }
    }
}
