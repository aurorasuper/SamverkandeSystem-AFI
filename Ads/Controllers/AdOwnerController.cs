using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Ads.Models;
using Newtonsoft.Json;
using System.Diagnostics;

namespace Ads.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdOwnerController : ControllerBase
    {
        private readonly Ads_DBContext _context;
        private const string SubscriberUri = "https://localhost:7125/api/Subscribers";
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
        public async Task<ActionResult<TblAdOwner>> GetSubscriber(int id)
        {
            // check if subscriber is already in Ads database. 
            TblAdOwner tblAdOwner = _context.TblAdOwners.Where(o => o.OwnSubId == id).FirstOrDefault();
            Subscriber sub = new Subscriber();

            // subscriber is not in database, get from subscribers system
            if (tblAdOwner == null)
            {
                //Make Http request to Subscribers system. Get subscriber info from id 
                using (HttpClient client = new HttpClient())
                {
                    try
                    {
                        HttpResponseMessage response = client.GetAsync(SubscriberUri + id).Result;

                        // catches 404 not found or other error messages 
                        response.EnsureSuccessStatusCode();

                        // read API response and store as local subscriber model 
                        var responseString = await response.Content.ReadAsStringAsync();
                        sub = JsonConvert.DeserializeObject<Subscriber>(responseString);

                        // add subscriber to tblOwners db
                        tblAdOwner.setSubscriber(sub);
                        _context.TblAdOwners.Add(tblAdOwner);
                        await _context.SaveChangesAsync();

                        return tblAdOwner;

                    }
                    catch (HttpRequestException e)
                    {

                        return NotFound(e.Message);
                    }
                }
            }

            return tblAdOwner;


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

            if ((bool)tblAdOwner.OwnIsSub)
            {
                Subscriber sub = tblAdOwner.GetSubscriber();
                var isSuccess = await PutSubscriberRequest(sub.SubId, sub);
                if (isSuccess != "")
                {
                    return NotFound(isSuccess);
                }   
            }
            return Ok(tblAdOwner);
        }

        //POST: api/AdOwner
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


        // POST: api/AdOwner/Company
        [HttpPost("Company")]
        public async Task<ActionResult<TblAdOwner>> PostCompany(Company company)
        {
            if (_context.TblAdOwners == null)
            {
                return Problem("Entity set 'Ads_DBContext.TblAdOwners'  is null.");
            }
            TblAdOwner tblAdOwner = new TblAdOwner();
            tblAdOwner.setCompany(company);
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

        private bool SubscriberExists(int id)
        {
            return (_context.TblAdOwners?.Any(e => e.OwnSubId == id)).GetValueOrDefault();
        }

        private async Task<String> PutSubscriberRequest(int id, Subscriber sub)
        {
            var responseMessage = "";
                using (HttpClient client = new HttpClient())
            {
                try
                {
                    var response = await client.PutAsJsonAsync<Subscriber>(SubscriberUri + "/" + id, sub);
                    
                    response.EnsureSuccessStatusCode();
                    

                }
                catch (HttpRequestException e)
                {
                    responseMessage = e.Message;
                    return await Task.FromResult(responseMessage);
                    
                }
                return await Task.FromResult(responseMessage);
            }
        }

        /*
        //PUT: api/adOwner/Subscriber
        [HttpPut("Subscriber/{id}")]
        public async Task<IActionResult> PutSubscriber(int id, Subscriber sub)
        {
            if (id != sub.SubId)
            {
                return BadRequest("Ids do not match");
            }
            // Store changes in sub model as TblAdowner model
            TblAdOwner tblAdOwner = _context.TblAdOwners.Where(o => o.OwnSubId == id).FirstOrDefault();
            if (tblAdOwner == null)
            {
                return BadRequest("tblAdOwner does not exist");
            }
            tblAdOwner.OwnName = sub.SubName;
            tblAdOwner.OwnPhone = sub.SubPhone;
            tblAdOwner.OwnDeliveryAdress = sub.SubDeliveryAdress;
            tblAdOwner.OwnDeliveryCounty = sub.SubDeliveryCounty;
            tblAdOwner.OwnDeliveryZip = sub.SubDeliveryZip;

            // modify stored adOwner 
            _context.Entry(tblAdOwner).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SubscriberExists(id))
                {
                    return NotFound("Subscriber Exists function error");
                }
                else
                {
                    throw;
                }
            }
            //update subscriber in subscriber system

            var isSuccess = await PutSubscriberRequest(id, sub);
            if (isSuccess != "")
            {
                return NotFound(isSuccess);
            }

            return Ok(tblAdOwner);
        }*/
    }
}
