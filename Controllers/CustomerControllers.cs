using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/customers")]
[ApiController]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public CustomerController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/customers
    [HttpGet]
    public IActionResult GetCustomers()
    {
        var customers = _context.Customers.ToList();
        return Ok(customers);
    }

    // GET: api/customers/{id}
    [HttpGet("{id}")]
    public IActionResult GetCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }
        return Ok(customer);
    }

    // POST: api/customers
    [HttpPost]
    public IActionResult CreateCustomer([FromBody] Customer customer)
    {
        if (customer == null)
        {
            return BadRequest();
        }

        _context.Customers.Add(customer);
        _context.SaveChanges();

        return CreatedAtAction("GetCustomer", new { id = customer.CustomerId }, customer);
    }

    // PUT: api/customers/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateCustomer(int id, [FromBody] Customer customer)
    {
        if (id != customer.CustomerId)
        {
            return BadRequest();
        }

        _context.Entry(customer).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/customers/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteCustomer(int id)
    {
        var customer = _context.Customers.Find(id);
        if (customer == null)
        {
            return NotFound();
        }

        _context.Customers.Remove(customer);
        _context.SaveChanges();

        return NoContent();
    }
}