using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;
using Microsoft.AspNetCore.Identity;

// This handles most API requests to the server about User Accounts
// so if the front end accesses https://<ip>:<port>/UserAccounts in different ways,
// different things will happen (get, post, delete http requests)
namespace Lab_E_Commerce_Website_API.Controllers
{
    public class LoginInfo
    {
        public int id { get; set; }

        public bool admin {  get; set; }
    }

    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DatabaseContext _context;

        private readonly PasswordHasher<User> passwordHasher = new PasswordHasher<User>();

        public UserController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetuserAccounts()
        {
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/<any existing user id>
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserAccount(int id)
        {
            var userAccount = await _context.Users.FindAsync(id);

            if (userAccount == null)
            {
                return NotFound();
            }

            return userAccount;
        }

        // GET: api/Users/LoginUser
        [HttpGet("LoginUser/{userName}/{password}")]
        public async Task<ActionResult<User>> LoginUserAccount(string userName, string password)
        {
            var userAccount = await _context.Users.Where<User>(thing => (thing.UserName == userName)).ToListAsync();

            bool ready = false;
            LoginInfo loginInfo = new LoginInfo();
            PasswordVerificationResult result;

            if (userAccount.Count > 0)
            {
                foreach (var user in userAccount)
                {
                    result = passwordHasher.VerifyHashedPassword(user, user.Password, password);
                    if (user.UserName != "" && user.UserName != null && user.Password != "" && user.Password != null && result == PasswordVerificationResult.Success)
                    {
                        loginInfo.admin = user.AdminPermission;
                        loginInfo.id = user.ID;
                        ready = true;
                        break;
                    }
                }
            }

            if (ready)
            {
                return Ok(loginInfo);
            }
            else
            {
                return NotFound();
            }
        }

        // PUT: api/Users/<any user id>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserAccount(int id, User userAccount)
        {
            if (id != userAccount.ID)
            {
                return BadRequest();
            }

            _context.Entry(userAccount).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserAccountExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUserAccount(User userAccount)
        {
            userAccount.Password = passwordHasher.HashPassword(userAccount, userAccount.Password);

            var matchResult = await _context.Users.Where<User>(userRow => userRow.UserName == userAccount.UserName
                                                                          && userRow.Password == userAccount.Password
                                                                          && userRow.PhoneNumber == userAccount.PhoneNumber
                                                                          && userRow.Address == userAccount.Address
                                                                          && userRow.Email == userAccount.Email).ToListAsync<User>();

            if (matchResult.Count == 0)
            {
                _context.Users.Add(userAccount);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetUserAccount", new { id = userAccount.ID }, userAccount);
            }

            return BadRequest();
        }

        // DELETE: api/Users/<any existing user id>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserAccount(int id)
        {
            var userAccount = await _context.Users.FindAsync(id);
            if (userAccount == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userAccount);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserAccountExists(int id)
        {
            return _context.Users.Any(e => e.ID == id);
        }
    }
}
