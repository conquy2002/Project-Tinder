#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_test.Data;
using api_test.Models;

namespace api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserContectsController : ControllerBase
    {
        private readonly api_testContext _context;

        public UserContectsController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/UserContects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserContect>>> GetUserContect()
        {
            return await _context.UserContect.ToListAsync();
        }

        // GET: api/UserContects/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UserContect>> GetUserContect(int id)
        {
            var userContect = await _context.UserContect.FindAsync(id);

            if (userContect == null)
            {
                return NotFound();
            }

            return userContect;
        }

        // PUT: api/UserContects/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserContect(int id, UserContect userContect)
        {
            if (id != userContect.Id)
            {
                return BadRequest();
            }

            _context.Entry(userContect).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserContectExists(id))
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

        // POST: api/UserContects
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<UserContect>> PostUserContect(UserContect userContect)
        {
            _context.UserContect.Add(userContect);
            await _context.SaveChangesAsync();

            return Ok(new {msg = "done"});
        }

        // DELETE: api/UserContects/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserContect(int id)
        {
            var userContect = await _context.UserContect.FindAsync(id);
            if (userContect == null)
            {
                return NotFound();
            }

            _context.UserContect.Remove(userContect);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserContectExists(int id)
        {
            return _context.UserContect.Any(e => e.Id == id);
        }
    }
}
