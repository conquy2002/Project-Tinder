#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_Tinder.Data;
using api_Tinder.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Cors;

namespace api_Tinder.Controllers
{
    [DisableCors]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public UsersController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<User>> GetUserdata(string? fles, int? id)
        {
            //if (id == null && string.IsNullOrEmpty(fles))
            //{
            //    return await _context.User.ToListAsync();
            //}

            if (id == null)
            {
                return NotFound();
            }
            var userseach = await _context.User.FindAsync(id);
            if (string.IsNullOrEmpty(fles))
            {
                if (userseach == null)
                {
                    return NotFound();
                }

                return userseach;
            }
            string[] words = fles.Split(",");
            foreach (var word in words)
            {
                switch (word)
                {
                    case "hobby":
                        var hobby = from m in _context.UserHobby
                                    where m.UserId == userseach.ID
                                    join n in _context.Interest
                                    on m.InterestID equals n.ID
                                    select n;
                        
                            foreach(var h in hobby)
                            {
                                userseach.UserHobbies.Add(h);
                            }
                        
                        
                        break;
                }
            }
            return userseach;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }
        

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.ID)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
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
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.ID }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}
