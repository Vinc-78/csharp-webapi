using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using csharp_webapi.Models;

namespace csharp_webapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SingolaAttivitasController : ControllerBase
    {
        private readonly SingolaAttivitaContext _context;

        public SingolaAttivitasController(SingolaAttivitaContext context)
        {
            _context = context;
        }

        // GET: api/SingolaAttivitas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SingolaAttivita>>> GetListaAttivita()
        {
          if (_context.ListaAttivita == null)
          {
              return NotFound();
          }
            return await _context.ListaAttivita.ToListAsync();
        }

        // GET: api/SingolaAttivitas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<SingolaAttivita>> GetSingolaAttivita(long id)
        {
          if (_context.ListaAttivita == null)
          {
              return NotFound();
          }
            var singolaAttivita = await _context.ListaAttivita.FindAsync(id);

            if (singolaAttivita == null)
            {
                return NotFound();
            }

            return singolaAttivita;
        }

        // PUT: api/SingolaAttivitas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSingolaAttivita(long id, SingolaAttivita singolaAttivita)
        {
            if (id != singolaAttivita.Id)
            {
                return BadRequest();
            }

            _context.Entry(singolaAttivita).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SingolaAttivitaExists(id))
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

        // POST: api/SingolaAttivitas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<SingolaAttivita>> PostSingolaAttivita(SingolaAttivita singolaAttivita)
        {
          if (_context.ListaAttivita == null)
          {
              return Problem("Entity set 'SingolaAttivitaContext.ListaAttivita'  is null.");
          }
            _context.ListaAttivita.Add(singolaAttivita);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSingolaAttivita", new { id = singolaAttivita.Id }, singolaAttivita);
        }

        // DELETE: api/SingolaAttivitas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSingolaAttivita(long id)
        {
            if (_context.ListaAttivita == null)
            {
                return NotFound();
            }
            var singolaAttivita = await _context.ListaAttivita.FindAsync(id);
            if (singolaAttivita == null)
            {
                return NotFound();
            }

            _context.ListaAttivita.Remove(singolaAttivita);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SingolaAttivitaExists(long id)
        {
            return (_context.ListaAttivita?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
