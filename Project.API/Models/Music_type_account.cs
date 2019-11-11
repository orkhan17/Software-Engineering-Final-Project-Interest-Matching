namespace Project.API.Models
{
    public class Music_type_account
    {
        public int Id { get; set; }
        public int Account_Id { get; set; }
        public int Music_type_id { get; set; }
        public Account Account { get; set; }
        public Music_type Music_type { get; set; }
    }
}