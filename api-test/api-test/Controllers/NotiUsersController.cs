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
    public class NotiUsersController : ControllerBase
    {
        private readonly api_testContext _context;

        public NotiUsersController(api_testContext context)
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
        public  object GetNotiUser(int id)
        {
            var notiUser =  _context.NotiUser.Where(i => i.UserId == id);

            if (notiUser == null)
            {
                return Ok(new {msg = "0"});
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
        public async Task<IActionResult> DeleteNotiUser(int id,int senderid)
        {
            var notiUser =  _context.NotiUser.Where(i => i.UserId == id && i.SenderId == senderid);

            if (notiUser == null)
            {
                return NotFound();
            }
            foreach (var a in notiUser)
            {
                _context.NotiUser.Remove(a);
            }
            
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotiUserExists(int id)
        {
            return _context.NotiUser.Any(e => e.Id == id);
        }
    }
}
