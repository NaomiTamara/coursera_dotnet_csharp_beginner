using Microsoft.AspNetCore.Mvc;
using UserManagementApi.Models;

namespace UserManagementApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private static readonly List<User> Users = new()
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

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return Ok(Users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "User ID must be greater than 0." });
            }

            var user = Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            return Ok(user);
        }

        [HttpPost]
        public ActionResult<User> CreateUser([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest(new { message = "User data is required." });
            }

            if (string.IsNullOrWhiteSpace(newUser.FirstName) ||
                string.IsNullOrWhiteSpace(newUser.LastName) ||
                string.IsNullOrWhiteSpace(newUser.Email) ||
                string.IsNullOrWhiteSpace(newUser.Department))
            {
                return BadRequest(new
                {
                    message = "FirstName, LastName, Email, and Department are required."
                });
            }

            if (Users.Any(u => u.Email.Equals(newUser.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new
                {
                    message = "A user with this email already exists."
                });
            }

            newUser.Id = Users.Any() ? Users.Max(u => u.Id) + 1 : 1;
            Users.Add(newUser);

            return CreatedAtAction(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "User ID must be greater than 0." });
            }

            if (updatedUser == null)
            {
                return BadRequest(new { message = "Updated user data is required." });
            }

            if (string.IsNullOrWhiteSpace(updatedUser.FirstName) ||
                string.IsNullOrWhiteSpace(updatedUser.LastName) ||
                string.IsNullOrWhiteSpace(updatedUser.Email) ||
                string.IsNullOrWhiteSpace(updatedUser.Department))
            {
                return BadRequest(new
                {
                    message = "FirstName, LastName, Email, and Department are required."
                });
            }

            var existingUser = Users.FirstOrDefault(u => u.Id == id);

            if (existingUser == null)
            {
                return NotFound(new { message = $"User with ID {id} not found." });
            }

            if (Users.Any(u => u.Id != id &&
                               u.Email.Equals(updatedUser.Email, StringComparison.OrdinalIgnoreCase)))
            {
                return BadRequest(new
                {
                    message = "Another user with this email already exists."
                });
            }

            existingUser.FirstName = updatedUser.FirstName;
            existingUser.LastName = updatedUser.LastName;
            existingUser.Email = updatedUser.Email;
            existingUser.Department = updatedUser.Department;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteUser(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "User ID must be greater than 0." });
            }

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