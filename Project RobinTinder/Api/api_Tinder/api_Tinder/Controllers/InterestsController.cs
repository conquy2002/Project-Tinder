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
    public class InterestsController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public InterestsController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/Interests
        [HttpGet]
        [DisableCors]
        public async Task<ActionResult<IEnumerable<Interest>>> GetInterest()
        {
            return await _context.Interest.ToListAsync();
        }

        // GET: api/Interests/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Interest>> GetInterest(int id)
        {
            var interest = await _context.Interest.FindAsync(id);

            if (interest == null)
            {
                return NotFound();
            }

            return interest;
        }

        // PUT: api/Interests/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInterest(int id, Interest interest)
        {
            if (id != interest.ID)
            {
                return BadRequest();
            }

            _context.Entry(interest).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InterestExists(id))
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

        // POST: api/Interests
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Interest>> PostInterest(Interest interest)
        {
            _context.Interest.Add(interest);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetInterest", new { id = interest.ID }, interest);
        }

        // DELETE: api/Interests/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInterest(int id)
        {
            var interest = await _context.Interest.FindAsync(id);
            if (interest == null)
            {
                return NotFound();
            }

            _context.Interest.Remove(interest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InterestExists(int id)
        {
            return _context.Interest.Any(e => e.ID == id);
        }
    }
}
