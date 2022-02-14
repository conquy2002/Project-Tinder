namespace api_test.Models
{
    public class UserHobby
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int HobbyId { get; set; }
        public DateTime Time { get; set; }
    }
}
