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
    public class NotiUsersController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public NotiUsersController(api_TinderContext context)
        {
            _context = context;
        }

        // GET: api/NotiUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotiUser>>> GetNotiUser()
        {
            return await _context.NotiUser.ToListAsync();
        }

        // GET: api/NotiUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<NotiUser>> GetNotiUser(int id)
        {
            var notiUser = await _context.NotiUser.FindAsync(id);

            if (notiUser == null)
            {
                return NotFound();
            }

            return notiUser;
        }

        // PUT: api/NotiUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotiUser(int id, NotiUser notiUser)
        {
            if (id != notiUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(notiUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotiUserExists(id))
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

        // POST: api/NotiUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<NotiUser>> PostNotiUser(NotiUser notiUser)
        {
            _context.NotiUser.Add(notiUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotiUser", new { id = notiUser.Id }, notiUser);
        }

        // DELETE: api/NotiUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotiUser(int id)
        {
            var notiUser = await _context.NotiUser.FindAsync(id);
            if (notiUser == null)
            {
                return NotFound();
            }

            _context.NotiUser.Remove(notiUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotiUserExists(int id)
        {
            return _context.NotiUser.Any(e => e.Id == id);
        }
    }
}
