using System;

namespace Project.API.DTOs
{
    public class AccountForUpdateDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime Birth_date { get; set; }
    }
}