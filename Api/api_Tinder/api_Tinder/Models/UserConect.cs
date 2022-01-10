namespace api_Tinder.Models
{
    public class UserConect
    {
        public int Id { get; set; }
        public string UserID { get; set; }
        public int UserConectID { get; set; }
        public string UserConectImager { get; set; }
        public string UserConectName { get; set; }
        public void UserConection(int id, string name ,string img)
        {
            UserConectID = id;
            UserConectImager = name;
            UserConectName = img;

        }

    }
}
