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
using Microsoft.AspNetCore.Cors;

namespace api_Tinder.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserConectsController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public UserConectsController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/UserConects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserConect>>> GetUserConect()
        {
            return await _context.UserConect.ToListAsync();
        }

        // GET: api/UserConects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserConect>> GetUserConect(int id)
        {
            var userConect = await _context.UserConect.FindAsync(id);

            if (userConect == null)
            {
                return NotFound();
            }

            return userConect;
        }

        // PUT: api/UserConects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserConect(int id, UserConect userConect)
        {
            if (id != userConect.Id)
            {
                return BadRequest();
            }

            _context.Entry(userConect).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserConectExists(id))
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

        // POST: api/UserConects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserConect>> PostUserConect(UserConect userConect)
        {
            _context.UserConect.Add(userConect);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserConect", new { id = userConect.Id }, userConect);
        }

        // DELETE: api/UserConects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserConect(int id)
        {
            var userConect = await _context.UserConect.FindAsync(id);
            if (userConect == null)
            {
                return NotFound();
            }

            _context.UserConect.Remove(userConect);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserConectExists(int id)
        {
            return _context.UserConect.Any(e => e.Id == id);
        }
    }
}
