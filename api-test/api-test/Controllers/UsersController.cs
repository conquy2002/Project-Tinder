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
    public class UsersController : ControllerBase
    {
        private readonly api_testContext _context;

        public UsersController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUser()
        {
            var user = await _context.User.ToListAsync();
            foreach (var item in user)
            {
                var userImgaer = from m in _context.ImagerUser
                                 where m.UserId == item.Id
                                 select m;
                foreach (var i in userImgaer)
                {
                    if (i != null)
                    {
                        var a = item.Imagers.FirstOrDefault(i => i.Id == i.Id);
                        if (a == null)
                        {
                            item.Imagers.Add(i);
                        }

                    }
                }
                var userhobby = from m in _context.UserHobby
                                where m.UserId == item.Id
                                select m;
                foreach (var hobby in userhobby)
                {
                    var hobbydo = await _context.Hobby.FirstOrDefaultAsync(i => i.Id == hobby.HobbyId);
                    item.UserHobbys.Add(hobby);
                    if (hobbydo != null) item.Hobbies.Add(hobbydo);
                }
            }
            return user;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }
            var userhobby = from m in _context.UserHobby
                            where m.UserId == id
                            select m;
            foreach (var hobby in userhobby)
            {
                var hobbydo = await _context.Hobby.FirstOrDefaultAsync(i => i.Id == hobby.HobbyId);
                user.UserHobbys.Add(hobby);
                if (hobbydo != null) user.Hobbies.Add(hobbydo);
            }
            var userImgaer= from m in _context.ImagerUser
                            where m.UserId == id
                            select m;
            var messgerUser =  _context.MessgerUser.Where(i => i.UserId == id).OrderByDescending(i => i.Time);
            foreach (var messger in messgerUser)
            {
                var b = user.MessgerUsers.Any(a => a.ThreadUserId == messger.ThreadUserId);
                if (b == false)
                {
                    user.MessgerUsers.Add(messger);
                }     
            }
          
            foreach (var i in userImgaer)
            {
                if (i != null)
                {
                    var a =  user.Imagers.FirstOrDefault(i => i.Id == i.Id);
                    if (a == null)
                    {
                        user.Imagers.Add(i);
                    }
                    
                }
            }
            var userContects = _context.UserContect.Where(i => i.UserId == id);

            foreach (var hobby in userContects)
            {
                var check = user.UserContects.Where(i => i.Id == hobby.Id);
                if (hobby != null && !check.Any()) user.UserContects.Add(hobby);
            }
            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(new {Startus = "Done"});
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<object> PostUser(User user)
        {
            var checklogin = await _context.User.FirstOrDefaultAsync(m => m.Account == user.Account);

            if (checklogin == null)
            {
                _context.User.Add(user);
                await _context.SaveChangesAsync();
                return new { Status = "Success", Message = "Thành công" };
            }
            return new { Status = "Error", Message = "Đã tồn tại" };
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.User.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.Id == id);
        }
    }
}
