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
    public class ImagerMessgersController : ControllerBase
    {
        private readonly api_testContext _context;

        public ImagerMessgersController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/ImagerMessgers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagerMessger>>> GetImagerMessger()
        {
            return await _context.ImagerMessger.ToListAsync();
        }

        // GET: api/ImagerMessgers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagerMessger>> GetImagerMessger(int id)
        {
            var imagerMessger = await _context.ImagerMessger.FindAsync(id);

            if (imagerMessger == null)
            {
                return NotFound();
            }

            return imagerMessger;
        }

        // PUT: api/ImagerMessgers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagerMessger(int id, ImagerMessger imagerMessger)
        {
            if (id != imagerMessger.Id)
            {
                return BadRequest();
            }

            _context.Entry(imagerMessger).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagerMessgerExists(id))
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

        // POST: api/ImagerMessgers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagerMessger>> PostImagerMessger(ICollection<ImagerMessger> imagerMessger)
        {
            foreach (var a in imagerMessger)
            {
                _context.ImagerMessger.Add(a);
            }

            await _context.SaveChangesAsync();

            return Ok(new { msg = "done"});
        }

        // DELETE: api/ImagerMessgers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagerMessger(int id)
        {
            var imagerMessger = await _context.ImagerMessger.FindAsync(id);
            if (imagerMessger == null)
            {
                return NotFound();
            }

            _context.ImagerMessger.Remove(imagerMessger);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagerMessgerExists(int id)
        {
            return _context.ImagerMessger.Any(e => e.Id == id);
        }
    }
}
