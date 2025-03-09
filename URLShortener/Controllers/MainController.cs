using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace URLShortener.Controllers
{
    public class MainController : ControllerBase
    {
        private readonly UrlDbContext _db;
        public MainController(UrlDbContext db)
        {
            _db = db;
        }

        [HttpPost("shorten")]
        public async Task<IActionResult> Shorten([FromBody] string url)
        {
            if (string.IsNullOrWhiteSpace(url)) return BadRequest();
            var shortCode = Guid.NewGuid().ToString()[..6];
            var urlEntry = new UrlEntry { ShortCode = shortCode, OriginalUrl = url };

            _db.Urls.Add(urlEntry);
            await _db.SaveChangesAsync();
            var shortUrl = $"{Request.Scheme}://{Request.Host}/api/url/{shortCode}";
            return Ok(new { ShortUrl = shortUrl });
        }

        [HttpGet("{shortCode}")]
        public async Task<IActionResult> RedirectToOriginal(string shortCode)
        {
            var entry = await _db.Urls.SingleOrDefaultAsync(u => u.ShortCode == shortCode);
            if (entry == null)
            {
                return NotFound();
            }
            return RedirectPermanent(entry.OriginalUrl);
        }

        [HttpGet("urls")]
        public async Task<IActionResult> GetAllUrls()
        {
            return Ok(await _db.Urls.ToListAsync());
        }
     
    }
}
