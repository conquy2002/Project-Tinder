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
    public class MessgerUsersController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public MessgerUsersController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/MessgerUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<MessgerUser>>> GetMessgerUser()
        {
            return await _context.MessgerUser.ToListAsync();
        }

        // GET: api/MessgerUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MessgerUser>> GetMessgerUser(int id)
        {
            var messgerUser = await _context.MessgerUser.FindAsync(id);

            if (messgerUser == null)
            {
                return NotFound();
            }

            return messgerUser;
        }

        // PUT: api/MessgerUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessgerUser(int id, MessgerUser messgerUser)
        {
            if (id != messgerUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(messgerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessgerUserExists(id))
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

        // POST: api/MessgerUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessgerUser>> PostMessgerUser(MessgerUser messgerUser)
        {
            _context.MessgerUser.Add(messgerUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessgerUser", new { id = messgerUser.Id }, messgerUser);
        }

        // DELETE: api/MessgerUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessgerUser(int id)
        {
            var messgerUser = await _context.MessgerUser.FindAsync(id);
            if (messgerUser == null)
            {
                return NotFound();
            }

            _context.MessgerUser.Remove(messgerUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessgerUserExists(int id)
        {
            return _context.MessgerUser.Any(e => e.Id == id);
        }
    }
}
