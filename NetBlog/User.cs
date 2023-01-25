using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace NetBlog
{
    public class User
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Username { get; set; }
        [MaxLength(20)]
        public string FirstName { get; set; }
        [MaxLength(20)]
        public string LastName { get; set; }
        [MaxLength(20)]
        public string Email { get; set; }
        [MaxLength(20)]
        public string Password { get; set; }
        [JsonIgnore]
        public virtual ICollection<Post> Posts { get; set;} = new List<Post>();
    }
}
