using System;

namespace Project.API.DTOs
{
    public class AccountForRegisterDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public DateTime Birth_date { get; set; }
        public DateTime Created_date { get; set; }
        public DateTime Last_active { get; set; }
        public int? Status { get; set; }

        public AccountForRegisterDto()
        {
            Created_date = DateTime.Now;
            Last_active = DateTime.Now;
        }
    }
}