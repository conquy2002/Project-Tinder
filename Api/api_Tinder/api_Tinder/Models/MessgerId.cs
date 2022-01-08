namespace api_Tinder.Models
{
    public class MessgerId
    {
        public int Id { get; set; }
        public int MessgerID { get; set; }
        public Messger Messger { get; set; }
        public string Body { get; set; }
        public int SenderID { get; set; }
        public DateTime Created { get; set; }
        public bool IsDeleted { get; set; }
    }
}
