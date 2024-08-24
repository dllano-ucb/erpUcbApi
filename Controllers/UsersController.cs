using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;
using erpUcbApi.Models;
using BCrypt.Net;

[Route("api/[controller]")]
[ApiController]
public class UsersController : Controller {
    private readonly PgContext _context;

    public UsersController(PgContext context) {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Users>>> GetUsers() {
        return await _context.Users.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Users>> GetUser(int id) {
        var user = await _context.Users.FindAsync(id);

        if (user == null) {
            return NotFound();
        }

        return user;
    }

    [HttpPost]
    public async Task<ActionResult<Users>> CreateUser(Users user) {
        user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
        user.CreatedAt = DateTime.UtcNow;
        user.UpdatedAt = DateTime.UtcNow;
        _context.Users.Add(user);

        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Users>> UpdateUser(int id, Users user) {
        if (id != user.Id) {
            return BadRequest();
        }

        user.UpdatedAt = DateTime.UtcNow;
        _context.Entry(user).State = EntityState.Modified;

        try {
            await _context.SaveChangesAsync();
        } catch (DbUpdateConcurrencyException) {
            if (!_context.Users.Any(u => u.Id == id)){
                return NotFound();
            }
        }

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult<Users>> DeleteUser(int id) {
        var user = await _context.Users.FindAsync(id);
        if (user == null) {
            return NotFound();
        }

        _context.Users.Remove(user);

        try {
            await _context.SaveChangesAsync();
        } catch (Exception ex) {
            return StatusCode(500, ex.ToString());
        }

        return NoContent();
    }
}