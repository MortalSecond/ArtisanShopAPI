using ArtisanShopAPI.Data;
using ArtisanShopAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ArtisanShopAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PricingController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PricingController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Pricing
        [HttpGet]
        public async Task<ActionResult<PricingConfig>> GetPricing()
        {
            // Always return the first pricing config
            var pricing = await _context.PricingConfigs.FirstOrDefaultAsync();

            if (pricing == null)
            {
                return NotFound(new { message = "Pricing configuration not found" });
            }

            return pricing;
        }

        // PUT: api/Pricing
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> UpdatePricing(PricingUpdateDto dto)
        {
            var pricing = await _context.PricingConfigs.FirstOrDefaultAsync();

            if (pricing == null)
            {
                return NotFound(new { message = "Pricing configuration not found" });
            }

            // Update only the provided fields
            if (dto.SizeSmall.HasValue) pricing.SizeSmall = dto.SizeSmall.Value;
            if (dto.SizeMedium.HasValue) pricing.SizeMedium = dto.SizeMedium.Value;
            if (dto.SizeLarge.HasValue) pricing.SizeLarge = dto.SizeLarge.Value;
            if (dto.SizeExtraLarge.HasValue) pricing.SizeExtraLarge = dto.SizeExtraLarge.Value;

            if (dto.FrameWooden.HasValue) pricing.FrameWooden = dto.FrameWooden.Value;
            if (dto.FrameMirror.HasValue) pricing.FrameMirror = dto.FrameMirror.Value;
            if (dto.FrameLedWooden.HasValue) pricing.FrameLedWooden = dto.FrameLedWooden.Value;
            if (dto.FrameLedMirror.HasValue) pricing.FrameLedMirror = dto.FrameLedMirror.Value;

            if (dto.StoneNone.HasValue) pricing.StoneNone = dto.StoneNone.Value;
            if (dto.StoneLight.HasValue) pricing.StoneLight = dto.StoneLight.Value;
            if (dto.StoneMedium.HasValue) pricing.StoneMedium = dto.StoneMedium.Value;
            if (dto.StoneHeavy.HasValue) pricing.StoneHeavy = dto.StoneHeavy.Value;

            if (dto.FeatureClock.HasValue) pricing.FeatureClock = dto.FeatureClock.Value;
            if (dto.FeatureDiamondStrips.HasValue) pricing.FeatureDiamondStrips = dto.FeatureDiamondStrips.Value;
            if (dto.FeatureStuds.HasValue) pricing.FeatureStuds = dto.FeatureStuds.Value;
            if (dto.FeatureLeds.HasValue) pricing.FeatureLeds = dto.FeatureLeds.Value;

            if (dto.TreatmentResin.HasValue) pricing.TreatmentResin = dto.TreatmentResin.Value;
            if (dto.TreatmentGeode.HasValue) pricing.TreatmentGeode = dto.TreatmentGeode.Value;

            if (dto.ShippingDomestic.HasValue) pricing.ShippingDomestic = dto.ShippingDomestic.Value;
            if (dto.ShippingNorthAmerica.HasValue) pricing.ShippingNorthAmerica = dto.ShippingNorthAmerica.Value;
            if (dto.ShippingInternational.HasValue) pricing.ShippingInternational = dto.ShippingInternational.Value;

            pricing.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return Ok(new { message = "Pricing updated successfully", pricing });
        }
    }

    // DTO
    public class PricingUpdateDto
    {
        public decimal? SizeSmall { get; set; }
        public decimal? SizeMedium { get; set; }
        public decimal? SizeLarge { get; set; }
        public decimal? SizeExtraLarge { get; set; }
        public decimal? FrameWooden { get; set; }
        public decimal? FrameMirror { get; set; }
        public decimal? FrameLedWooden { get; set; }
        public decimal? FrameLedMirror { get; set; }
        public decimal? StoneNone { get; set; }
        public decimal? StoneLight { get; set; }
        public decimal? StoneMedium { get; set; }
        public decimal? StoneHeavy { get; set; }
        public decimal? FeatureClock { get; set; }
        public decimal? FeatureDiamondStrips { get; set; }
        public decimal? FeatureStuds { get; set; }
        public decimal? FeatureLeds { get; set; }
        public decimal? TreatmentResin { get; set; }
        public decimal? TreatmentGeode { get; set; }
        public decimal? ShippingDomestic { get; set; }
        public decimal? ShippingNorthAmerica { get; set; }
        public decimal? ShippingInternational { get; set; }
    }
}