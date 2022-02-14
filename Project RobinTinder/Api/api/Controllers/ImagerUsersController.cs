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
using api_test.Controllers;


namespace api_test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagerUsersController : ControllerBase
    {
        private readonly api_testContext _context;

        public ImagerUsersController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/ImagerUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagerUser>>> GetImagerUser()
        {
            return await _context.ImagerUser.ToListAsync();
        }

        // GET: api/ImagerUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagerUser>> GetImagerUser(int id)
        {
            var imagerUser = await _context.ImagerUser.FindAsync(id);

            if (imagerUser == null)
            {
                return NotFound();
            }

            return imagerUser;
        }

        // PUT: api/ImagerUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagerUser(int id, ImagerUser imagerUser)
        {
            if (id != imagerUser.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagerUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagerUserExists(id))
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

        // POST: api/ImagerUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost("{id}")]
        public async Task<ActionResult<ImagerUser>> PostImagerUser(ICollection<ImagerUser> imagerUser,int id)
        {
            var hobbies = from m in _context.ImagerUser
                          where m.UserId == id
                          select m.Id;
            foreach (var i in hobbies)
            {
                var userHobby3 = await _context.ImagerUser.FindAsync(i);
                if (userHobby3 != null)
                {
                    _context.ImagerUser.Remove(userHobby3);
                }
            }
            foreach (var i in imagerUser)
            {
                _context.ImagerUser.Add(i);
            }
            
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ImagerUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagerUser(int id)
        {
            var imagerUser = await _context.ImagerUser.FindAsync(id);
            if (imagerUser == null)
            {
                return NotFound();
            }
            var folderName = Path.Combine("Images");
            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathSave, imagerUser.Imager);
            System.IO.File.Delete(fullPath);
            _context.ImagerUser.Remove(imagerUser);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool ImagerUserExists(int id)
        {
            return _context.ImagerUser.Any(e => e.Id == id);
        }
    }
}
