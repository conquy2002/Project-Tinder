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
    public class HobbiesController : ControllerBase
    {
        private readonly api_testContext _context;

        public HobbiesController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/Hobbies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Hobby>>> GetHobby()
        {
            return await _context.Hobby.ToListAsync();
        }

        // GET: api/Hobbies/5
        [HttpGet("{id}")]
        public async Task<Object> GetHobby(int id)
        {
            var hobby = await _context.Hobby.FindAsync(id);
            var hobbies =  _context.UserHobby.Where(i => i.HobbyId == id);
            ICollection<User> User = new List<User>();
            if (hobby == null)
            {
                return NotFound();
            }
            foreach (var h in hobbies)
            {
                var user = await _context.User.FindAsync(h.UserId);
                var userImgaer = from m in _context.ImagerUser
                                 where m.UserId == user.Id
                                 select m;
                foreach (var i in userImgaer)
                {
                    if (i != null)
                    {
                        var a = user.Imagers.FirstOrDefault(i => i.Id == i.Id);
                        if (a == null)
                        {
                            user.Imagers.Add(i);
                        }

                    }
                }
                var userhobby = from m in _context.UserHobby
                                where m.UserId == user.Id
                                select m;
                foreach (var hobby2 in userhobby)
                {
                    var hobbydo = await _context.Hobby.FirstOrDefaultAsync(i => i.Id == hobby2.HobbyId);
                    if (hobbydo != null) user.Hobbies.Add(hobbydo);
                }
                User.Add(user);
            }
            return User.ToList();
        }

        // PUT: api/Hobbies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHobby(int id, Hobby hobby)
        {
            if (id != hobby.Id)
            {
                return BadRequest();
            }

            _context.Entry(hobby).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HobbyExists(id))
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

        // POST: api/Hobbies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Hobby>> PostHobby(Hobby hobby)
        {

                _context.Hobby.Add(hobby);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetHobby", new { id = hobby.Id }, hobby);

        }

        // DELETE: api/Hobbies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHobby(int id)
        {
            var hobby = await _context.Hobby.FindAsync(id);
            if (hobby == null)
            {
                return NotFound();
            }

            _context.Hobby.Remove(hobby);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HobbyExists(int id)
        {
            return _context.Hobby.Any(e => e.Id == id);
        }
    }
}
