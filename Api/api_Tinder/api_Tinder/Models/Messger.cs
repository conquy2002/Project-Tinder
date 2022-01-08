namespace api_Tinder.Models
{
    public class Messger
    {
        public int Id { get; set; } 
        public int UserID { get; set; }
        public User User { get; set; }
        public string Body { get; set; }
        public int UserIdconnect { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
    }
}
