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
    public class ImagerOfUsersController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public ImagerOfUsersController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/ImagerOfUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagerOfUser>>> GetImagerOfUser()
        {
            return await _context.ImagerOfUser.ToListAsync();
        }

        // GET: api/ImagerOfUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagerOfUser>> GetImagerOfUser(int id)
        {
            var imagerOfUser = await _context.ImagerOfUser.FindAsync(id);

            if (imagerOfUser == null)
            {
                return NotFound();
            }

            return imagerOfUser;
        }

        // PUT: api/ImagerOfUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagerOfUser(int id, ImagerOfUser imagerOfUser)
        {
            if (id != imagerOfUser.ID)
            {
                return BadRequest();
            }

            _context.Entry(imagerOfUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagerOfUserExists(id))
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

        // POST: api/ImagerOfUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagerOfUser>> PostImagerOfUser(ImagerOfUser imagerOfUser)
        {
            _context.ImagerOfUser.Add(imagerOfUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagerOfUser", new { id = imagerOfUser.ID }, imagerOfUser);
        }

        // DELETE: api/ImagerOfUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagerOfUser(int id)
        {
            var imagerOfUser = await _context.ImagerOfUser.FindAsync(id);
            if (imagerOfUser == null)
            {
                return NotFound();
            }

            _context.ImagerOfUser.Remove(imagerOfUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagerOfUserExists(int id)
        {
            return _context.ImagerOfUser.Any(e => e.ID == id);
        }
    }
}
