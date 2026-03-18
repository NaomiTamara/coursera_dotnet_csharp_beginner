using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        // In-memory user list for demo purposes
        private static readonly List<User> Users = new List<User>
        {
            new User
            {
                Id = 1,
                FirstName = "Alice",
                LastName = "Johnson",
                Email = "alice.johnson@techhive.com",
                Department = "HR"
            },
            new User
            {
                Id = 2,
                FirstName = "Bob",
                LastName = "Smith",
                Email = "bob.smith@techhive.com",
                Department = "IT"
            }
        };

        // GET: api/users
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(Users);
        }

        // GET: api/users/1
        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public ActionResult<User> CreateUser(User newUser)
        {
            if (string.IsNullOrWhiteSpace(newUser.FirstName) ||
                string.IsNullOrWhiteSpace(newUser.LastName) ||
                string.IsNullOrWhiteSpace(newUser.Email))
            {
                return BadRequest(new { message = "FirstName, LastName, and Email are required." });
            }

            newUser.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        // PUT: api/users/1
        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, User updatedUser)
        {
            var existingUser = Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Department = updatedUser.Department;

            return NoContent();
        }

        // DELETE: api/users/1
        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            Users.Remove(user);

            return NoContent();
        }
    }
}