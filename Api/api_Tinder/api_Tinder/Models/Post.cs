namespace api_Tinder.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int ImageID { get; set; }
        public ImageUser Image { get; set; }
        public int UserID { get; set; }
        public User User { get; set; }
        public int NumberConnect { get; set; }
        public DateTime CreatedDate { get; set; }   
    }
}
