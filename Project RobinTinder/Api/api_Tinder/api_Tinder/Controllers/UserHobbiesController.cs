#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_Tinder.Data;
using Microsoft.AspNetCore.Cors;
using api_Tinder.Models;

namespace api_Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserHobbiesController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public UserHobbiesController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/UserHobbies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserHobby>>> GetUserHobby()
        {
            return await _context.UserHobby.ToListAsync();
        }

        // GET: api/UserHobbies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserHobby>> GetUserHobby(int id)
        {
            var userHobby = await _context.UserHobby.FindAsync(id);

            if (userHobby == null)
            {
                return NotFound();
            }

            return userHobby;
        }

        // PUT: api/UserHobbies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserHobby(int id, UserHobby userHobby)
        {
            if (id != userHobby.ID)
            {
                return BadRequest();
            }

            _context.Entry(userHobby).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserHobbyExists(id))
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

        // POST: api/UserHobbies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserHobby>> PostUserHobby(UserHobby userHobby)
        {
            _context.UserHobby.Add(userHobby);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserHobby", new { id = userHobby.ID }, userHobby);
        }

        // DELETE: api/UserHobbies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserHobby(int id)
        {
            var userHobby = await _context.UserHobby.FindAsync(id);
            if (userHobby == null)
            {
                return NotFound();
            }

            _context.UserHobby.Remove(userHobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserHobbyExists(int id)
        {
            return _context.UserHobby.Any(e => e.ID == id);
        }
    }
}
