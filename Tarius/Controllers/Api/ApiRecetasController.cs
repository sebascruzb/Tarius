using Microsoft.AspNetCore.Mvc;
using Tarius.Models.Colaborador;
using Tarius.Data;
using Microsoft.EntityFrameworkCore;

namespace Tarius.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class ApiRecetasController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ApiRecetasController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Receta>>> GetRecetas()
        {
            return await _context.Recetas.Include(r => r.Pasos).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Receta>> GetReceta(int id)
        {
            var receta = await _context.Recetas
                .Include(r => r.Pasos)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (receta == null)
            {
                return NotFound();
            }

            return receta;
        }

        [HttpPost]
        public async Task<ActionResult<Receta>> PostReceta(Receta receta)
        {
            _context.Recetas.Add(receta);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetReceta), new { id = receta.Id }, receta);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutReceta(int id, Receta receta)
        {
            if (id != receta.Id)
                return BadRequest();

            _context.Entry(receta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Recetas.Any(r => r.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteReceta(int id)
        {
            var receta = await _context.Recetas.FindAsync(id);
            if (receta == null)
                return NotFound();

            _context.Recetas.Remove(receta);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
