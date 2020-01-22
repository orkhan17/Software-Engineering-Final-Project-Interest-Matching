using System;

namespace Project.API.DTOs
{
    public class PostToAddDto
    {
        public int AccountId { get; set; }
        public string Text { get; set; }
        public string Video_link { get; set; }
        public DateTime Created_date { get; set; }
        public int Status { get; set; }
        public PostToAddDto()
        {
            Created_date = DateTime.Now;
            Status = 1;
        }
    }
}