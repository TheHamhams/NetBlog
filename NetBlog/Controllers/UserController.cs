using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace NetBlog.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {
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

        private readonly DataContext _context;

        public UserController(DataContext context)
        {
            _context = context;
        }

        // GET Requests
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            return Ok(await _context.Users.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User ID not found");
            }
            return Ok(user);
        }

        // POST Requests
        [HttpPost]
        public async Task<ActionResult<User>> AddUser(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok(user);
        }

        // PUT Requests
        [HttpPut("{id}")]
        public async Task<ActionResult<List<User>>> UpdateUser(int id, User request)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User ID not found");
            }

            user.Username = request.Username;
            user.FirstName = request.FirstName;
            user.LastName = request.LastName;
            user.Email = request.Email;
            user.Password = request.Password;

            await _context.SaveChangesAsync(); 

            return Ok(user);
        }

        // DELETE Requests
        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteHero(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return BadRequest("User ID not found");
            }

            string username = user.Username;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return Ok($"{username} removed");
        }
    }
}
