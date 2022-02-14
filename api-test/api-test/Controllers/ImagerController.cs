using api_test.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;

namespace api_test.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ImagerController : ControllerBase
    {
        private readonly api_testContext _context;
        public ImagerController(api_testContext context)
        {
            _context = context;
        }
        [HttpPost, DisableRequestSizeLimit]
       public IActionResult Upload(IFormFile file)
       {
            try
            {
                var folderName = Path.Combine("Images");
                var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }
                    return Ok(new { Startus = "Done" , xem = pathSave });

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" lỗi: ${ex}");
            }
       }
        [HttpGet]
        public IActionResult GetImger(string? name)
        {
            var folderName = Path.Combine("Images");
            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            if (name == null)
            {
                var fullPath2 = Path.Combine(pathSave, "nofound.jpg");
                Byte[] b = System.IO.File.ReadAllBytes(fullPath2);
                return File(b, "image/jpeg");
            }
           
            var fullPath = Path.Combine(pathSave, name);
            if (System.IO.File.Exists(fullPath))
            {
                Byte[] b = System.IO.File.ReadAllBytes(fullPath);
                return File(b, "image/jpeg");
            }
            else
            {
                fullPath = Path.Combine(pathSave, "nofound.jpg");
                Byte[] b = System.IO.File.ReadAllBytes(fullPath);
                return File(b, "image/jpeg");
            }
        }
        [HttpGet("{id}")]
        public IActionResult GetImgerid(int id)
        {
            var name = id + "_avt.jpg";
            var folderName = Path.Combine("Images");
            var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var fullPath = Path.Combine(pathSave, name);
            
            if (System.IO.File.Exists(fullPath))
            {
                Byte[] b = System.IO.File.ReadAllBytes(fullPath);
                return File(b, "image/jpeg");
            }
            else
            {
                fullPath = Path.Combine(pathSave, "nofound.jpg");
                Byte[] b = System.IO.File.ReadAllBytes(fullPath);
                return File(b, "image/jpeg");
            }
            
        }
        [HttpDelete]

        public IActionResult Delete(string name)
        {
            try
            {
                var folderName = Path.Combine("Images");
                var pathSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
                var fullPath = Path.Combine(pathSave, name);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                  
                    return Ok(new { Startus = "Done" });

                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $" lỗi: ${ex}");
            }
        }
    }
}
