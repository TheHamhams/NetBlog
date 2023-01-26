using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace NetBlog.Controllers
{
    [Route("api/post")]
    [ApiController]
    public class PostController : ControllerBase
    {
        // Sample Data
        public static List<User> _users = new List<User>
        {
            new User
            {
                Id= 1,
                Username = "Hamhams",
                FirstName = "Corey",
                LastName = "Hamren",
                Email = "Hamhams86@gmail.com",
                Password = "Guest"
            },
            new User
            {
                Id= 2,
                Username = "OtherDude928347",
                FirstName = "Random",
                LastName = "Gai",
                Email = "FlavorTown@test.com",
                Password = "Guest"
            }
        };

        public static List<Post> _posts = new List<Post>
        {
            new Post
            {
                Id = 1,
                Title = "Title 1",
                Description = "Description 1",
                UserId = 1,
                Created = DateTime.Now
            },
            new Post 
            {
                Id = 2,
                Title = "Title 2",
                Description = "Description 2",
                UserId = 2,
                Created = DateTime.Now
            },
            new Post
            {
                Id = 3,
                Title = "Title 3",
                Description = "Description 3",
                UserId = 1,
                Created = DateTime.Now
            },
        };

        private readonly DataContext _context;

        public PostController(DataContext context)
        {
            _context = context;
        }

        // GET Requests

        [HttpGet]
        public async Task<ActionResult<List<Post>>> Get()
        {
            return Ok(await _context.Posts.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Post>>> Get(int id)
        {
            var posts = _context.Posts.Where(p => p.UserId == id).ToList();
            if (posts == null)
            {
                return BadRequest("No posts found for that ID");
            }

            return Ok(posts);
        }

        // POST Requests

        [HttpPost]
        public async Task<ActionResult<Post>> AddPost(Post request)
        {
            var user = await _context.Users.FindAsync(request.UserId);
            if (user == null)
            {
                return BadRequest("User ID not found");
            }
            var post = new Post
            {
                Title = request.Title,
                Description = request.Description,
                UserId = request.UserId,
                Created = DateTime.UtcNow
            };

            _context.Posts.Add(post);
            user.Posts.Add(post);

            await _context.SaveChangesAsync();

            return Ok(post);


        }

        // DELETE Requests
        [HttpDelete("{id}")]
        public async Task<ActionResult<Post>> Delete(int id)
        {
            var post = await _context.Posts.FindAsync(id);
            if (post == null)
            {
                return BadRequest("Post ID not found");
            }

            var user = await _context.Users.FindAsync(post.UserId);
            if (user == null)
            {
                return BadRequest("User Id not found");
            }

            _context.Posts.Remove(post);
            user.Posts.Remove(post);

            await _context.SaveChangesAsync();
            return Ok("Post Removed");
        }

    }

    
}
