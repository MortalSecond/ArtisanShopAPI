using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ArtisanShopAPI.Data;
using ArtisanShopAPI.Models;
using ArtisanShopAPI.Services;
using Microsoft.AspNetCore.Authorization;

namespace ArtisanShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public ContactController(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // GET: api/Contact
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactInquiry>>> GetContactInquiries()
        {
            return await _context.ContactInquiries.ToListAsync();
        }

        // GET: api/Contact/5
        [Authorize]
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInquiry>> GetContactInquiry(int id)
        {
            var inquiry = await _context.ContactInquiries.FindAsync(id);

            if (inquiry == null)
                return NotFound();

            return inquiry;
        }

        // POST: api/Contact
        [HttpPost]
        public async Task<ActionResult<ContactInquiry>> SubmitContact(ContactInquiry inquiry)
        {
            // Validation
            if (string.IsNullOrWhiteSpace(inquiry.Name) ||
                string.IsNullOrWhiteSpace(inquiry.Email) ||
                string.IsNullOrWhiteSpace(inquiry.Message))
            {
                return BadRequest(new { message = "Name, email, and message are required" });
            }

            // Inside a Try for the sake of responsiveness to errors.
            try
            {
                // Save to database
                _context.ContactInquiries.Add(inquiry);
                await _context.SaveChangesAsync();

                // Send email
                await _emailService.SendContactEmailAsync(inquiry);

                return Ok(new
                {
                    message = "Thank you for your message! We'll get back to you within 24-48 hours.",
                    success = true
                });
            }
            catch (Exception ex)
            {
                // Log the error
                Console.WriteLine($"Error processing contact form: {ex.Message}");

                return StatusCode(500, new
                {
                    message = "There was an error sending your message. Please try again or email us directly.",
                    success = false
                });
            }
        }

        // PATCH: api/Contact/5/mark-read
        [Authorize]
        [HttpPatch("{id}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            var inquiry = await _context.ContactInquiries.FindAsync(id);

            if (inquiry == null)
                return NotFound();

            inquiry.IsRead = true;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // DELETE: api/Contact/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInquiry(int id)
        {
            var inquiry = await _context.ContactInquiries.FindAsync(id);

            if (inquiry == null)
                return NotFound();

            _context.ContactInquiries.Remove(inquiry);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
