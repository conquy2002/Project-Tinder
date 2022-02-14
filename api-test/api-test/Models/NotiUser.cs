namespace api_test.Models
{
    public class NotiUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? Body { get; set; }
        public int SenderId { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
