namespace api_test.Models
{
    public class MessgerUser
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? ThreadID { get; set; }
        public string? Body { get; set; }
        public bool Self { get; set; }
        public int ThreadUserId { get; set; }
        public DateTime Time { get; set; } = DateTime.Now;
        public MessgerBody? MessgerBodies { get; set; }
    }
}
