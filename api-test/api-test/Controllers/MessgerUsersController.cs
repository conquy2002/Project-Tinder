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
    public class MessgerUsersController : ControllerBase
    {
        private readonly api_testContext _context;

        public MessgerUsersController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/MessgerUsers/5
        [HttpGet("{id}")]
        public object GetMessgerUser(int id,string threadid)
        {
            var messgerUser = _context.MessgerUser.Where(i => i.UserId == id).OrderByDescending(i => i.Time);
            if (messgerUser == null)
            {
                return Ok(new {});
            }
            if(threadid != null)
            {
                messgerUser = (IOrderedQueryable<MessgerUser>)messgerUser.Where(i => i.ThreadID == threadid);
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
        public async Task<ActionResult<MessgerUser>> PostMessgerUser(ICollection<MessgerUser> messgerUsers)
        {
            foreach(var messgerUser in messgerUsers)
            {
                var hobbies = from m in _context.MessgerUser
                              where m.UserId == messgerUser.UserId
                              where m.ThreadID == messgerUser.ThreadID
                              select m;
                if (hobbies.Any())
                {
                    foreach (var a in hobbies)
                    {
                        _context.MessgerUser.Remove(a);
                    }
                }
                _context.MessgerUser.Add(messgerUser);
            }
            await _context.SaveChangesAsync();

            return Ok(new {msg = "done"});
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
