using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using api_Tinder.Data;
using api_Tinder.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;

namespace api_Tinder.Controllers
{
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly api_TinderContext _context;

        public AccountController(api_TinderContext context)
        {
            _context = context;
        }
        [Route("Api/Login/UserLogin")]
        [HttpPost]
        public object PostLogin(LoginViewModel user)
        {
            var checkLogin = from m in _context.LoginViewModel
                             where m.Email == user.Email
                             where m.Password == user.Password
                             select m;
            if (checkLogin.Count() == 0)
            {
                return new { Status = "Inactive", Message = "User Inactive." };
            }
            else
            {
                return new { Status = "Success", Message = user.ID };
            }
        }

        [Route("Api/Login/Createcontact")]
        [HttpPost]
        public async Task<object> Createcontact(RegistrationModel user)
        {
            var checkLogin = from m in _context.RegistrationModel
                             where m.Email == user.Email
                             select m;
            if (checkLogin.Count() == 0)
            {
                _context.RegistrationModel.Add(user);
                await _context.SaveChangesAsync();
                CreatedAtAction("PostLogin", new { user = user.LoginViewModels }, user.LoginViewModels);
                return new { Status = "Success", Message = user.ID };
            }
            else
            {
                return new { Status = "Error", Message = "Invalid Data." };
            }
        }
        [Route("Api/Login/UserLoginwithToken/{user}")]
        [HttpGet]
        public object GetLoginToken(string user)
        {
            var checkLogin = from m in _context.logintoken
                             where m.token == user
                             select m;
            if (checkLogin.Count() == 0)
            {
                return new { Status = "Error", Message = "login fail." };
            }
            else
            {
                return new { Status = "Success", Message = checkLogin };
            }
        }
        [Route("Api/Login/UserLoginwithToken")]
        [HttpPost]
        public async Task<object> PostLoginToken(logintoken toekn)
        {
            _context.logintoken.Add(toekn);
           return await _context.SaveChangesAsync();
        }
        [Route("Api/Login/UserLoginwithToken/{token}")]
        [HttpDelete]
        public async Task<object> DeleteLoginToken(string token)
        {
            var checkLogin = await _context.logintoken.FindAsync(token);
            if (checkLogin == null)
            {
                return new { Status = "error", Message = "logout fail." };
            }
            else
            {
                _context.logintoken.Remove(checkLogin);
                await _context.SaveChangesAsync();
                return new { Status = "Success", Message = "logout success" };
            }
        }
    }
}
