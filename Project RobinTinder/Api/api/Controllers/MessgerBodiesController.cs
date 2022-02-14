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
    public class MessgerBodiesController : ControllerBase
    {
        private readonly api_testContext _context;

        public MessgerBodiesController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/MessgerBodies
        [HttpGet]
        public object GetMessgerBody(int UserId, int userconnectid)
        {
            var check = _context.MessgerUser.Any(a => a.UserId == UserId && a.ThreadUserId == userconnectid);
            var check2 =  _context.MessgerUser.Any(a => a.UserId == userconnectid && a.ThreadUserId == UserId);
            if (check)
            {
                var data = _context.MessgerUser.Where(a => a.UserId == UserId && a.ThreadUserId == userconnectid).Select(m => m.ThreadID);
                return data;
            }
            else
            {
                if (check2)
                {
                    var data = _context.MessgerUser.Where(a => a.UserId == userconnectid && a.ThreadUserId == UserId).Select(m => m.ThreadID);
                    return data;
                }
                else
                {
                    return Ok(new { msg = "done" });
                }
            }
        }

        // GET: api/MessgerBodies/5
        [HttpGet("{id}")]
        public  object GetMessgerBody(string id, int? number)
        {
            int limit = 10;
            if(number != null)
            {
                limit = (int)(number * limit);
            }
            
            
            var messgerBody =  ( from m in _context.MessgerBody
                              where m.ThreadID == id
                              orderby m.Id descending
                              select m).Take(limit);

            if (messgerBody == null)
            {
                return NotFound();
            }
            foreach(var a in messgerBody)
            {
                var img = from m in _context.ImagerMessger
                          where m.Time == a.Time
                          select m;
                foreach(var b in img)
                {
                    var c = a.Imagers.FirstOrDefault(i => i.Id == i.Id);
                    if(c == null) a.Imagers.Add(b);
                }  
            }
            return messgerBody;
        }

        // PUT: api/MessgerBodies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMessgerBody(int id, MessgerBody messgerBody)
        {
            if (id != messgerBody.Id)
            {
                return BadRequest();
            }

            _context.Entry(messgerBody).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MessgerBodyExists(id))
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

        // POST: api/MessgerBodies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<MessgerBody>> PostMessgerBody(MessgerBody messgerBody)
        {
            _context.MessgerBody.Add(messgerBody);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMessgerBody", new { id = messgerBody.Id }, messgerBody);
        }

        // DELETE: api/MessgerBodies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMessgerBody(int id)
        {
            var messgerBody = await _context.MessgerBody.FindAsync(id);
            if (messgerBody == null)
            {
                return NotFound();
            }

            _context.MessgerBody.Remove(messgerBody);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MessgerBodyExists(int id)
        {
            return _context.MessgerBody.Any(e => e.Id == id);
        }
    }
}
