using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtisanShopAPI.Data;
using ArtisanShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using ArtisanShopAPI.Services;

namespace ArtisanShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaintingsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IBlobStorageService _blobStorageService;

        public PaintingsController(AppDbContext context, IBlobStorageService blobStorageService)
        {
            _context = context;
            _blobStorageService = blobStorageService;
        }

        // GET: api/Paintings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Painting>>> GetPaintings()
        {
            return await _context.Paintings.OrderBy(p=>p.Id).ToListAsync();
        }

        // GET: api/Paintings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Painting>> GetPainting(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
            {
                return NotFound();
            }

            return painting;
        }

        // PUT: api/Paintings/5
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePainting(int id, [FromForm] PaintingUpdateDto dto)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
                return NotFound();

            // Update fields
            if (!string.IsNullOrEmpty(dto.Title))
                painting.Title = dto.Title;

            if (!string.IsNullOrEmpty(dto.Details))
                painting.Details = dto.Details;

            if (dto.Height.HasValue)
                painting.Height = dto.Height.Value;

            if (dto.Width.HasValue)
                painting.Width = dto.Width.Value;

            if (dto.Price.HasValue)
                painting.Price = dto.Price.Value;

            if (dto.Available.HasValue)
                painting.Available = dto.Available.Value;

            if (dto.ImageFile != null && dto.ImageFile.Length > 0)
            {
                // Delete old image
                var oldFileName = painting.ImageURL?.Split('/').Last();
                if (!string.IsNullOrEmpty(oldFileName))
                {
                    await _blobStorageService.DeleteImageAsync(oldFileName);
                }

                // Upload new image
                using var stream = dto.ImageFile.OpenReadStream();
                painting.ImageURL = await _blobStorageService.UploadImageAsync(
                    stream,
                    dto.ImageFile.FileName
                );
            }

            try
            {
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaintingExists(id))
                    return NotFound();
                throw;
            }
        }

        // POST: api/Paintings/upload
        [Authorize]
        [HttpPost("upload")]
        public async Task<ActionResult<Painting>> UploadPainting([FromForm] PaintingUploadDto dto)
        {
            if (dto.ImageFile == null || dto.ImageFile.Length == 0)
                return BadRequest(new { message = "Image file is required" });

            try
            {
                // Upload image to blob storage
                string imageUrl;
                using (var stream = dto.ImageFile.OpenReadStream())
                {
                    imageUrl = await _blobStorageService.UploadImageAsync(
                        stream,
                        dto.ImageFile.FileName
                    );
                }

                var painting = new Painting
                {
                    Title = dto.Title,
                    Details = dto.Details,
                    Height = dto.Height ?? 0,
                    Width = dto.Width ?? 0,
                    Price = dto.Price ?? 0,
                    ImageURL = imageUrl,
                    Available = true
                };

                _context.Paintings.Add(painting);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetPainting), new { id = painting.Id }, painting);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error uploading painting", error = ex.Message });
            }
        }

        // DELETE: api/Paintings/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePainting(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);
            if (painting == null)
            {
                return NotFound();
            }

            // Delete image from blob storage
            if (!string.IsNullOrEmpty(painting.ImageURL))
            {
                var fileName = painting.ImageURL.Split('/').Last();
                await _blobStorageService.DeleteImageAsync(fileName);
            }

            _context.Paintings.Remove(painting);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Paintings/5/availability
        [Authorize]
        [HttpPatch("{id}/availability")]
        public async Task<IActionResult> ToggleAvailability(int id)
        {
            var painting = await _context.Paintings.FindAsync(id);

            if (painting == null)
                return NotFound();

            painting.Available = !painting.Available;
            await _context.SaveChangesAsync();

            return Ok(new { available = painting.Available });
        }

        private bool PaintingExists(int id)
        {
            return _context.Paintings.Any(e => e.Id == id);
        }

        // DTOs
        public class PaintingUploadDto
        {
            public string Title { get; set; }
            public string Details { get; set; }
            public int? Height { get; set; }
            public int? Width { get; set; }
            public int? Price { get; set; }
            public IFormFile ImageFile { get; set; }
        }

        public class PaintingUpdateDto
        {
            public string? Title { get; set; }
            public string? Details { get; set; }
            public int? Height { get; set; }
            public int? Width { get; set; }
            public int? Price { get; set; }
            public bool? Available { get; set; }
            public IFormFile? ImageFile { get; set; }
        }
    }
}
