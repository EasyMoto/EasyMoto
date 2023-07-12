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
    public class UtilizadoresAPIController : ControllerBase
    {
        private readonly ApplicationDbContext _bd;

        public UtilizadoresAPIController(ApplicationDbContext context)
        {
            _bd = context;
        }

        // GET: api/UtilizadoresAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Utilizadores>>> GetUtilizadores()
        {
            return await _bd.Utilizadores.ToListAsync();
        }

        // GET: api/UtilizadoresAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Utilizadores>> GetUtilizadores(int id)
        {
            var utilizadores = await _bd.Utilizadores.FindAsync(id);

            if (utilizadores == null)
            {
                return NotFound();
            }

            return utilizadores;
        }

        // PUT: api/UtilizadoresAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUtilizadores(int id, Utilizadores utilizadores)
        {
            if (id != utilizadores.Id)
            {
                return BadRequest();
            }

            _bd.Entry(utilizadores).State = EntityState.Modified;

            try
            {
                await _bd.SaveChangesAsync();
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

        // POST: api/UtilizadoresAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Utilizadores>> PostUtilizadores(Utilizadores utilizadores)
        {
            _bd.Utilizadores.Add(utilizadores);
            await _bd.SaveChangesAsync();

            return CreatedAtAction("GetUtilizadores", new { id = utilizadores.Id }, utilizadores);
        }

        // DELETE: api/UtilizadoresAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUtilizadores(int id)
        {
            var utilizadores = await _bd.Utilizadores.FindAsync(id);
            if (utilizadores == null)
            {
                return NotFound();
            }

            _bd.Utilizadores.Remove(utilizadores);
            await _bd.SaveChangesAsync();

            return NoContent();
        }

        private bool UtilizadoresExists(int id)
        {
            return _bd.Utilizadores.Any(e => e.Id == id);
        }
    }
}
