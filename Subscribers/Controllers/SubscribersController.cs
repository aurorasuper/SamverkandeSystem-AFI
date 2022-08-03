using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Subscribers.Models;

namespace Subscribers.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscribersController : ControllerBase
    {
        private readonly Subscription_DBContext _context;

        public SubscribersController(Subscription_DBContext context)
        {
            _context = context;
        }

        // GET: api/Subscribers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblSubscriber>>> GetTblSubscribers()
        {
          if (_context.TblSubscribers == null)
          {
              return NotFound();
          }
            return await _context.TblSubscribers.ToListAsync();
        }

        // GET: api/Subscribers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TblSubscriber>> GetTblSubscriber(int id)
        {
          if (_context.TblSubscribers == null)
          {
              return NotFound();
          }
            var tblSubscriber = await _context.TblSubscribers.FindAsync(id);

            if (tblSubscriber == null)
            {
                return NotFound();
            }

            return tblSubscriber;
        }

        // PUT: api/Subscribers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTblSubscriber(int id, TblSubscriber tblSubscriber)
        {
            if (id != tblSubscriber.SubId)
            {
                return BadRequest();
            }

            _context.Entry(tblSubscriber).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TblSubscriberExists(id))
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

        // POST: api/Subscribers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TblSubscriber>> PostTblSubscriber(TblSubscriber tblSubscriber)
        {
          if (_context.TblSubscribers == null)
          {
              return Problem("Entity set 'Subscription_DBContext.TblSubscribers'  is null.");
          }
            _context.TblSubscribers.Add(tblSubscriber);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTblSubscriber", new { id = tblSubscriber.SubId }, tblSubscriber);
        }

        // DELETE: api/Subscribers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTblSubscriber(int id)
        {
            if (_context.TblSubscribers == null)
            {
                return NotFound();
            }
            var tblSubscriber = await _context.TblSubscribers.FindAsync(id);
            if (tblSubscriber == null)
            {
                return NotFound();
            }

            _context.TblSubscribers.Remove(tblSubscriber);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TblSubscriberExists(int id)
        {
            return (_context.TblSubscribers?.Any(e => e.SubId == id)).GetValueOrDefault();
        }
    }
}
