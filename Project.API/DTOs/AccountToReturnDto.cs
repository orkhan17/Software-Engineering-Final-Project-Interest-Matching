using System;

namespace Project.API.DTOs
{
    public class AccountToReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Gender { get; set; }
        public DateTime Birth_date { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Last_active { get; set; }
    }
}