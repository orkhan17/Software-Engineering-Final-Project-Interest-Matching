using System.Collections.Generic;

namespace Project.API.Models
{
    public class Music_type
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public ICollection<Music_type_account> Music_types { get; set; }
    }
}