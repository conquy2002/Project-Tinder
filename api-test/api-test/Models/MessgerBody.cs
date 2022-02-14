namespace api_test.Models
{
    public class MessgerBody
    {
        public int Id { get; set; }
        public string? ThreadID { get; set; }
        public int SenderId { get; set; }
        public string? Body { get; set; }
        public DateTime Time { get; set; }
        public virtual ICollection<ImagerMessger> Imagers { get; set; } = new List<ImagerMessger>();
        
    }
}
