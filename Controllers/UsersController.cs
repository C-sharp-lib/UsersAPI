using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeAPI.Models;

namespace PracticeAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly ApplicationDbContext _context;
        public UsersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
            try
            {
                if (users != null)
                {
                    return Ok(users);
                }
            }
            catch (Exception ex)
            {
                NotFound(ex);
            }
            return BadRequest();
        }

        [HttpGet("{userId}")]
        public async Task<IActionResult> GetUser(int userId) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
            if (user == null) 
            {
                return NotFound("There was no user with the id of: " + userId);
            }
            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(Users user) 
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Users users = new Users
                    {
                        Name = user.Name,
                        Email = user.Email,
                        UserName = user.UserName,
                        Password = user.Password,
                        Phone = user.Phone
                    };
                    _context.Users.Add(users);
                    await _context.SaveChangesAsync();
                }
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, string name, string email, string username, string password, string phone) 
        {
            try
            {
                
                if (ModelState.IsValid)
                {
                    Users? user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                    user.Name = name;
                    user.Email = email;
                    user.UserName = username;
                    user.Password = password;
                    user.Phone = phone;
                    _context.Users.Update(user);
                    await _context.SaveChangesAsync();
                    return Ok("User Updated Successfully");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteUser(int userId) 
        {
            try
            {
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
                if (user != null) 
                {
                    _context.Users.Remove(user);
                }
                await _context.SaveChangesAsync();
            }
            catch (Exception ex) 
            {
                return BadRequest(ex.Message);
            }
            return Ok("User deleted successfully.");
        }
    }
}
