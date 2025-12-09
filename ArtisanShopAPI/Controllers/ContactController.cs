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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ContactInquiry>>> GetContactInquiries()
        {
            return await _context.ContactInquiries.ToListAsync();
        }

        // GET: api/Contact/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ContactInquiry>> GetContactInquiry(int id)
        {
            var contactInquiry = await _context.ContactInquiries.FindAsync(id);

            if (contactInquiry == null)
            {
                return NotFound();
            }

            return contactInquiry;
        }

        // PUT: api/Contact/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContactInquiry(int id, ContactInquiry contactInquiry)
        {
            if (id != contactInquiry.Id)
            {
                return BadRequest();
            }

            _context.Entry(contactInquiry).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactInquiryExists(id))
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

        // POST: api/Contact
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ContactInquiry>> SubmitContact(ContactInquiry inquiry)
        {
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
                // Log the error (you'd use a proper logger in production)
                Console.WriteLine($"Error processing contact form: {ex.Message}");

                return StatusCode(500, new
                {
                    message = "There was an error sending your message. Please try again or email us directly.",
                    success = false
                });
            }
        }

        // DELETE: api/Contact/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContactInquiry(int id)
        {
            var contactInquiry = await _context.ContactInquiries.FindAsync(id);
            if (contactInquiry == null)
            {
                return NotFound();
            }

            _context.ContactInquiries.Remove(contactInquiry);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ContactInquiryExists(int id)
        {
            return _context.ContactInquiries.Any(e => e.Id == id);
        }
    }
}
