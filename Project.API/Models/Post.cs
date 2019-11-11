using System;

namespace Project.API.Models
{
    public class Post
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Text { get; set; }
        public string Video_link { get; set; }
        public DateTime Created_date { get; set; }
        public Account Account { get; set; }

    }
}