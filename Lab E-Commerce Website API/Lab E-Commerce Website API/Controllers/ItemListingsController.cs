using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API.Models;

// this is a controller to handle the API requests from the front end about listings
// so if the front end accesses https://<ip>:<port>/UserAccounts in different ways,
// different things will happen (get, post, delete http requests)
namespace Lab_E_Commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemListingsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public ItemListingsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/ItemListings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ItemListing>>> GetItemListing()
        {
            return await _context.ItemListings.ToListAsync();
        }

        // GET: api/ItemListings/<any listing id>
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemListing>> GetItemListing(int id)
        {
            var itemListing = await _context.ItemListings.FindAsync(id);

            if (itemListing == null)
            {
                return NotFound();
            }

            return itemListing;
        }

        // PUT: api/ItemListings/<any listing id>
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemListing(int id, ItemListing itemListing)
        {
            if (id != itemListing.ID)
            {
                return BadRequest();
            }

            _context.Entry(itemListing).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemListingExists(id))
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

        // POST: api/ItemListings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemListing>> PostItemListing(ItemListing itemListing)
        {
            var matchResult = await _context.ItemListings.Where<ItemListing>(itemRow => itemRow.OwnerID == itemListing.OwnerID
                                                                                        && itemRow.Price == itemListing.Price
                                                                                        && itemRow.Name == itemListing.Name
                                                                                        && itemRow.Description == itemListing.Description
                                                                                        && itemRow.Category == itemListing.Category).ToListAsync<ItemListing>();

            if (matchResult.Count == 0)
            {
                itemListing.DateOfPosting = DateTime.UtcNow;

                _context.ItemListings.Add(itemListing);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetItemListing", new { id = itemListing.ID }, itemListing);
            }

            return BadRequest();
        }

        // DELETE: api/ItemListings/<any listing id>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemListing(int id)
        {
            var itemListing = await _context.ItemListings.FindAsync(id);
            if (itemListing == null)
            {
                return NotFound();
            }

            _context.ItemListings.Remove(itemListing);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ItemListingExists(int id)
        {
            return _context.ItemListings.Any(e => e.ID == id);
        }
    }
}
