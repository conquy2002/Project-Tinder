namespace api_Tinder.Models
{
    public class MessgerUser
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string Imager { get; set; }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public void To(int id, string name)
        {
            UserId = id;
            UserName = name;
        }

        public int SenderId { get; set; }
        public string SenderName { get; set; }
        public void Form(int id, string name)
        {
            SenderId = id;
            SenderName = name;
        }
    }
}
