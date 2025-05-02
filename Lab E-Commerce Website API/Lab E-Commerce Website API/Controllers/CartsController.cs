using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lab_E_Commerce_Website_API;
using Lab_E_Commerce_Website_API.Models;

namespace Lab_E_Commerce_Website_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public CartsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Carts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cart>>> Getcarts()
        {
            return await _context.Carts.ToListAsync();
        }

        // GET: api/Carts/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cart>> GetCartRow(int id)
        {
            var cart = await _context.Carts.FindAsync(id);

            if (cart == null)
            {
                return NotFound();
            }

            return cart;
        }

        // GET: api/Carts/CartLookup/5
        [HttpGet("CartLookup/{id}")]
        public async Task<ActionResult<List<ItemListing>>> GetCartOfUser(int id)
        {
            var carts = await _context.Carts.Where<Cart>(cart => cart.UserID == id).ToListAsync<Cart>();

            List<ItemListing> items = new List<ItemListing>();
            List<int> previousCheckedListings = new List<int>();

            foreach (var cart in carts)
            {
                if (!previousCheckedListings.Contains(cart.ListingID))
                {
                    previousCheckedListings.Add(cart.ListingID);
                    var item = await _context.ItemListings.FindAsync(cart.ListingID);
                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }
            

            if (items.Count <= 0)
            {
                return NotFound();
            }

            ActionResult<List<ItemListing>> itemsToReturn = new ActionResult<List<ItemListing>>(items);

            return itemsToReturn;
        }

        // PUT: api/Carts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCart(int id, Cart cart)
        {
            if (id != cart.ID)
            {
                return BadRequest();
            }

            _context.Entry(cart).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CartExists(id))
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

        // POST: api/Carts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Cart>> PostCart(Cart cart)
        {
            var matchResult = await _context.Carts.Where<Cart>(cartRow => cartRow.UserID == cart.UserID && cartRow.ListingID == cart.ListingID).ToListAsync<Cart>();

            if (matchResult.Count == 0)
            {
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetCartRow", new { id = cart.ID }, cart);
            }
            
            return BadRequest();
        }

        // DELETE: api/Carts/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCart(int id)
        {
            var cart = await _context.Carts.FindAsync(id);
            if (cart == null)
            {
                return NotFound();
            }

            _context.Carts.Remove(cart);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CartExists(int id)
        {
            return _context.Carts.Any(e => e.ID == id);
        }
    }
}
