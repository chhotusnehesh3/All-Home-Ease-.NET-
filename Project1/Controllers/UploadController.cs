using Microsoft.AspNetCore.Mvc;
using Project1.Models;

namespace Project1.Controllers
{
    [Route("admin")]
    public class UploadController : Controller
    {
        private readonly AllHomeEaseContext _context;

        public UploadController(AllHomeEaseContext context)
        {
            _context = context;
        }

        [HttpGet("services/{id}/upload")]
        public IActionResult UploadImagePage(string id)
        {
            ViewData["ServiceId"] = id;
            return View("UploadImage");
        }

        [HttpPost("services/{id}/upload")]
        public async Task<IActionResult> UploadImage(string id, IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("File not selected");

            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                byte[] imageBytes = memoryStream.ToArray();

                ImgTb img = new ImgTb
                {
                    ImpSize = imageBytes,
                    Name = file.FileName,
                    Type = file.ContentType
                };

                _context.ImgTbs.Add(img);
                await _context.SaveChangesAsync();

                ServiceImg serviceImg = new ServiceImg
                {
                    ServiceId = long.Parse(id),
                    ImgId = img.Id
                };

                _context.ServiceImgs.Add(serviceImg);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index", "ServiceTbs");
            }
        }
    }
}
