using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ads.Models;
using Newtonsoft.Json;

namespace Ads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdOwnerController : ControllerBase
    {
        private readonly Ads_DBContext _context;

        public AdOwnerController(Ads_DBContext context)
        {
            _context = context;
        }

        // GET: api/AdOwner
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblAdOwner>>> GetTblAdOwners()
        {
          if (_context.TblAdOwners == null)
          {
              return NotFound();
          }
            return await _context.TblAdOwners.ToListAsync();
        }

        //GET: api/AdOwner/Subscriber/Id
        [HttpGet("Subscriber/{id}")]
        public async Task<ActionResult<String>> GetSubscriber(int id)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    HttpResponseMessage response = client.GetAsync("https://localhost:7125/api/Subscribers/", (HttpCompletionOption)id).Result;
                    response.EnsureSuccessStatusCode();
                    var responseString = await response.Content.ReadAsStringAsync();
                    var sub = JsonConvert.DeserializeObject<String>(responseString);
                    //return responseString;
                    return sub;
         
                }catch(HttpRequestException e)
                {
                    return NotFound(e);
                }
            }
        }

        // GET: api/AdOwner/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblAdOwner>> GetTblAdOwner(int id)
        {
          if (_context.TblAdOwners == null)
          {
              return NotFound();
          }
            var tblAdOwner = await _context.TblAdOwners.FindAsync(id);

            if (tblAdOwner == null)
            {
                return NotFound();
            }

            return tblAdOwner;
        }

        // PUT: api/AdOwner/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblAdOwner(int id, TblAdOwner tblAdOwner)
        {
            if (id != tblAdOwner.OwnId)
            {
                return BadRequest();
            }

            _context.Entry(tblAdOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblAdOwnerExists(id))
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

        // POST: api/AdOwner
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblAdOwner>> PostTblAdOwner(TblAdOwner tblAdOwner)
        {
          if (_context.TblAdOwners == null)
          {
              return Problem("Entity set 'Ads_DBContext.TblAdOwners'  is null.");
          }
            _context.TblAdOwners.Add(tblAdOwner);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblAdOwner", new { id = tblAdOwner.OwnId }, tblAdOwner);
        }

        // DELETE: api/AdOwner/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblAdOwner(int id)
        {
            if (_context.TblAdOwners == null)
            {
                return NotFound();
            }
            var tblAdOwner = await _context.TblAdOwners.FindAsync(id);
            if (tblAdOwner == null)
            {
                return NotFound();
            }

            _context.TblAdOwners.Remove(tblAdOwner);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblAdOwnerExists(int id)
        {
            return (_context.TblAdOwners?.Any(e => e.OwnId == id)).GetValueOrDefault();
        }
    }
}
