namespace api_test.Models
{
    public class ImagerUser
    {
        public int Id { get; set; }
        public string? Body { get; set; }
        public int UserId { get; set; }
        public string? Imager { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
