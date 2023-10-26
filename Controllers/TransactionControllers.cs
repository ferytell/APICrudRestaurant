using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/transactions")]
[ApiController]
public class TransactionController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public TransactionController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/transactions
    [HttpGet]
    public IActionResult GetTransactions()
    {
        var transactions = _context.Transactions
            .Include(t => t.Customer)
            .Include(t => t.Food)
            .Select(t => new TransactionDTO
            {
                TransactionId = t.TransactionId,
                CustomerId = t.CustomerId,
                FoodId = t.FoodId,
                TransactionDate = t.TransactionDate.ToString(), // Convert to string or DateTime as needed
                Quantity = t.Quantity,
                CustomerName = t.Customer.FirstName, // Include only the "name" property
                FoodName = t.Food.Name // Include only the "name" property
            })
            .ToList();
        return Ok(transactions);
    }

    // GET: api/transactions/{id}
    [HttpGet("{id}")]
    public IActionResult GetTransaction(int id)
    {
        var transaction = _context.Transactions.Find(id);
        if (transaction == null)
        {
            return NotFound();
        }
        return Ok(transaction);
    }

    // POST: api/transactions
    [HttpPost]
    public IActionResult CreateTransaction([FromBody] Transaction transaction)
    {
        if (transaction == null)
        {
            return BadRequest();
        }

        _context.Transactions.Add(transaction);
        _context.SaveChanges();

        return CreatedAtAction("GetTransaction", new { id = transaction.TransactionId }, transaction);
    }

    // PUT: api/transactions/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
    {
        if (id != transaction.TransactionId)
        {
            return BadRequest();
        }

        _context.Entry(transaction).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/transactions/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteTransaction(int id)
    {
        var transaction = _context.Transactions.Find(id);
        if (transaction == null)
        {
            return NotFound();
        }

        _context.Transactions.Remove(transaction);
        _context.SaveChanges();

        return NoContent();
    }
}