namespace Project.API.Models
{
    public class Follower
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public int Following_AccountId { get; set; }
    }
}