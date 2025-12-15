using ArtisanShopAPI.Data;
using ArtisanShopAPI.Models;
using ArtisanShopAPI.Services;
using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArtisanShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommissionController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;
        private readonly IBlobStorageService _blobStorageService;

        public CommissionController(AppDbContext context, IEmailService emailService, IBlobStorageService blobStorageService)
        {
            _context = context;
            _emailService = emailService;
            _blobStorageService = blobStorageService;
        }

        // GET: api/Commission
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CommissionRequest>>> GetCommissionRequests()
        {
            return await _context.CommissionRequests.ToListAsync();
        }

        // GET: api/Commission/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<CommissionRequest>> GetCommissionRequest(int id)
        {
            var commissionRequest = await _context.CommissionRequests.FindAsync(id);

            if (commissionRequest == null)
            {
                return NotFound();
            }

            return commissionRequest;
        }

        // POST: api/Commission
        [HttpPost]
        public async Task<ActionResult<CommissionRequest>> SubmitCommissionRequest([FromForm] CommissionRequestDto dto)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(dto.Name) ||
                string.IsNullOrWhiteSpace(dto.Email))
            {
                return BadRequest(new { message = "Name and email are required" });
            }

            try
            {
                var request = new CommissionRequest
                {
                    Name = dto.Name,
                    Email = dto.Email,
                    Phone = dto.Phone,
                    Message = dto.Message,
                    Size = dto.Size,
                    StoneCoverage = dto.StoneCoverage,
                    Frame = dto.Frame,
                    Features = dto.Features,
                    Treatments = dto.Treatments,
                    Shipping = dto.Shipping,
                    EstimatedPrice = dto.EstimatedPrice
                };

                // Handle image upload if provided
                if (dto.PaintingImage != null && dto.PaintingImage.Length > 0)
                {
                    using var stream = dto.PaintingImage.OpenReadStream();
                    var imageUrl = await _blobStorageService.UploadImageAsync(
                        stream,
                        dto.PaintingImage.FileName
                    );
                    request.ImageUrl = imageUrl;
                }

                // Save to database
                _context.CommissionRequests.Add(request);
                await _context.SaveChangesAsync();

                // Send email
                await _emailService.SendCommissionRequestEmailAsync(request);

                return Ok(new
                {
                    message = "Thank you for your commission request! We'll review your specifications and get back to you within 24-48 hours with a detailed quote.",
                    success = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing commission request: {ex.Message}");
                return StatusCode(500, new
                {
                    message = "There was an error processing your request. Please try again or contact us directly.",
                    success = false
                });
            }
        }

        // DELETE: api/Commission/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCommissionRequest(int id)
        {
            var commissionRequest = await _context.CommissionRequests.FindAsync(id);
            if (commissionRequest == null)
            {
                return NotFound();
            }

            // Delete associated image if exists
            if (!string.IsNullOrEmpty(commissionRequest.ImageUrl))
            {
                var fileName = commissionRequest.ImageUrl.Split('/').Last();
                await _blobStorageService.DeleteImageAsync(fileName);
            }

            _context.CommissionRequests.Remove(commissionRequest);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // PATCH: api/Commission/5/mark-read
        [Authorize]
        [HttpPatch("{id}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var request = await _context.CommissionRequests.FindAsync(id);

            if (request == null)
                return NotFound();

            request.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Form DTO
        public class CommissionRequestDto
        {
            public string Name { get; set; }
            public string Email { get; set; }
            public string? Phone { get; set; }
            public string? Message { get; set; }
            public string Size { get; set; }
            public string StoneCoverage { get; set; }
            public string Frame { get; set; }
            public string? Features { get; set; }
            public string? Treatments { get; set; }
            public string Shipping { get; set; }
            public decimal EstimatedPrice { get; set; }
            public IFormFile? PaintingImage { get; set; }
        }
    }
}
