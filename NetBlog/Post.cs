using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace NetBlog
{
    public class Post
    {
        public int Id { get; set; }
        [MaxLength(20)]
        public string Title { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public DateTime? Created { get; set; }
    }
}
