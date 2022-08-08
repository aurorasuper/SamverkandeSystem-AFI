using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ads.Models;

namespace Ads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly Ads_DBContext _context;

        public AdsController(Ads_DBContext context)
        {
            _context = context;
        }

        // GET: api/Ads
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAd>>> GetTblAds()
        {
          if (_context.TblAds == null)
          {
              return NotFound();
          }
            return await _context.TblAds.ToListAsync();
        }

        // GET: api/Ads/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Ad>> GetTblAd(int id)
        {
          if (_context.TblAds == null)
          {
              return NotFound();
          }
            var tblAd = await _context.TblAds.FindAsync(id);

            if (tblAd == null)
            {
                return NotFound();
            }

            Ad ad = tblAd.GetAd();
            return ad;
        }

        // PUT: api/Ads/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAd(int id, Ad ad)
        {
            if (id != ad.AdId)
            {
                return BadRequest();
            }
            TblAd tblAd = _context.TblAds.Where(o => o.AdId == id).FirstOrDefault();
            tblAd.SetTblAd(ad);
            _context.Entry(tblAd).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAdExists(id))
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

        // POST: api/Ads
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Ad>> PostTblAd(Ad ad)
        {
          if (_context.TblAds == null)
          {
              return Problem("Entity set 'Ads_DBContext.TblAds'  is null.");
          }
            
            var adOwner = _context.TblAdOwners.Where(o => o.OwnId == ad.AdOwnerId).FirstOrDefault();

            if( adOwner == null)
            {
                return BadRequest("Ad Owner does not exist");
            }

            if ((bool)adOwner.OwnIsSub)
            {
                ad.AdCost = 0;
            }
            else
            {
                ad.AdCost = 40;
            }
            TblAd tblAd = new();
            tblAd.SetTblAd(ad);

            // set an ads ad owner 
            tblAd.AdOwner = adOwner;

            // add the ad to the ad owners list of ads
            adOwner.TblAds.Add(tblAd);
            
            // add the ad to the db
            _context.TblAds.Add(tblAd);
            
            await _context.SaveChangesAsync();
            ad.AdId = tblAd.AdId;

            return CreatedAtAction("GetTblAd", new { id = tblAd.AdId }, ad);
        }

        // DELETE: api/Ads/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblAd(int id)
        {
            if (_context.TblAds == null)
            {
                return NotFound();
            }
            var tblAd = await _context.TblAds.FindAsync(id);
            if (tblAd == null)
            {
                return NotFound();
            }

            _context.TblAds.Remove(tblAd);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblAdExists(int id)
        {
            return (_context.TblAds?.Any(e => e.AdId == id)).GetValueOrDefault();
        }
    }
}
