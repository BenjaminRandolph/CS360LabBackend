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
    public class TransactionsController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public TransactionsController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: api/Transactions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Transaction>>> Gettransactions()
        {
            return await _context.transactions.ToListAsync();
        }

        // GET: api/Transactions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Transaction>> GetTransaction(int id)
        {
            var transaction = await _context.transactions.FindAsync(id);

            if (transaction == null)
            {
                return NotFound();
            }

            return transaction;
        }

        // PUT: api/Transactions/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransaction(int id, Transaction transaction)
        {
            if (id != transaction.id)
            {
                return BadRequest();
            }

            _context.Entry(transaction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionExists(id))
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

        // POST: api/Transactions
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Transaction>> PostTransaction(Transaction transaction)
        {
            var matchResult = await _context.transactions.Where<Transaction>(transactionRow => transactionRow.posterid == transaction.posterid
                                                                                               && transactionRow.productname == transaction.productname
                                                                                               && transactionRow.productdescription == transaction.productdescription
                                                                                               && transactionRow.amountpaid == transaction.amountpaid
                                                                                               && transactionRow.amountofproduct == transaction.amountofproduct
                                                                                               && transactionRow.category == transaction.category
                                                                                               && transactionRow.purchaserid == transaction.purchaserid
                                                                                               && transactionRow.dateofpurchase == transaction.dateofpurchase).ToListAsync<Transaction>();

            if (matchResult.Count == 0)
            {
                _context.transactions.Add(transaction);
                await _context.SaveChangesAsync();

                return CreatedAtAction("GetTransaction", new { id = transaction.id }, transaction);
            }

            return BadRequest();
        }

        // DELETE: api/Transactions/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransaction(int id)
        {
            var transaction = await _context.transactions.FindAsync(id);
            if (transaction == null)
            {
                return NotFound();
            }

            _context.transactions.Remove(transaction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionExists(int id)
        {
            return _context.transactions.Any(e => e.id == id);
        }
    }
}
