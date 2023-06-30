using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EasyMoto.Data;
using EasyMoto.Models;

namespace EasyMoto.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Utilizadores1Controller : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public Utilizadores1Controller(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Utilizadores1
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizadores>>> GetUtilizadores()
        {
            return await _context.Utilizadores.ToListAsync();
        }

        // GET: api/Utilizadores1/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizadores>> GetUtilizadores(int id)
        {
            var utilizadores = await _context.Utilizadores.FindAsync(id);

            if (utilizadores == null)
            {
                return NotFound();
            }

            return utilizadores;
        }

        // PUT: api/Utilizadores1/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizadores(int id, Utilizadores utilizadores)
        {
            if (id != utilizadores.Id)
            {
                return BadRequest();
            }

            _context.Entry(utilizadores).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UtilizadoresExists(id))
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

        // POST: api/Utilizadores1
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilizadores>> PostUtilizadores(Utilizadores utilizadores)
        {
            _context.Utilizadores.Add(utilizadores);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUtilizadores", new { id = utilizadores.Id }, utilizadores);
        }

        // DELETE: api/Utilizadores1/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizadores(int id)
        {
            var utilizadores = await _context.Utilizadores.FindAsync(id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            _context.Utilizadores.Remove(utilizadores);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilizadoresExists(int id)
        {
            return _context.Utilizadores.Any(e => e.Id == id);
        }
    }
}
