using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using APICrudRestaurant.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

[Route("api/food")]
[ApiController]
public class FoodController : ControllerBase
{
    private readonly ApplicationDbContext _context;

    public FoodController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: api/food
    [HttpGet]
    public IActionResult GetFoods()
    {
        var foods = _context.Foods.ToList();
        return Ok(foods);
    }

    // GET: api/food/{id}
    [HttpGet("{id}")]
    public IActionResult GetFood(int id)
    {
        var food = _context.Foods.Find(id);
        if (food == null)
        {
            return NotFound();
        }
        return Ok(food);
    }

    // POST: api/food
    [HttpPost]
    public IActionResult CreateFood([FromBody] Food food)
    {
        if (food == null)
        {
            return BadRequest();
        }

        _context.Foods.Add(food);
        _context.SaveChanges();

        return CreatedAtAction("GetFood", new { id = food.FoodId }, food);
    }

    // PUT: api/food/{id}
    [HttpPut("{id}")]
    public IActionResult UpdateFood(int id, [FromBody] Food food)
    {
        if (id != food.FoodId)
        {
            return BadRequest();
        }

        _context.Entry(food).State = EntityState.Modified;
        _context.SaveChanges();

        return NoContent();
    }

    // DELETE: api/food/{id}
    [HttpDelete("{id}")]
    public IActionResult DeleteFood(int id)
    {
        var food = _context.Foods.Find(id);
        if (food == null)
        {
            return NotFound();
        }

        _context.Foods.Remove(food);
        _context.SaveChanges();

        return NoContent();
    }
}