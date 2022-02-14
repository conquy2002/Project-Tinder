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
        //[Route("Api/Login/UserLoginwithToken/{user}")]
        
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly api_testContext _context;

        public LoginsController(api_testContext context)
        {
            _context = context;
        }

        // GET: api/Logins
        [HttpGet]
        public async Task<Object> GetLoginToken(string user)
        {
            var checkLogin = await _context.User.FirstOrDefaultAsync(u => u.Token == user);
            if (checkLogin == null)
            {
                return new { Status = "Error", Message = "login fail." };
            }
            else
            {
                return new { Status = "Success", Message = checkLogin.Id };
            } 
        }
    }
}
