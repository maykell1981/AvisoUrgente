using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoApi.Models;

namespace ProjetoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProcessosController : ControllerBase
    {
        private readonly AvisoUrgenteContext _context;

        public ProcessosController(AvisoUrgenteContext context)
        {
            _context = context;
        }

        // GET: api/Processos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Processos>>> GetProcessos()
        {
            return await _context.Processos.ToListAsync();
        }

        // GET: api/Processos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Processos>> GetProcessos(string id)
        {
            var processos = await _context.Processos.FindAsync(id);

            if (processos == null)
            {
                return NotFound();
            }

            return processos;
        }

        // PUT: api/Processos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessos(string id, Processos processos)
        {
            if (id != processos.NumeroProcesso)
            {
                return BadRequest();
            }

            _context.Entry(processos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessosExists(id))
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

        // POST: api/Processos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Processos>> PostProcessos(Processos processos)
        {

            var NumeroProcesso = _context.Processos
                                .Where(n => n.NumeroProcesso == processos.NumeroProcesso)
                                .Select(n => n.NumeroProcesso)
                                .FirstOrDefault();

            if (NumeroProcesso != null)
            {
                return BadRequest("Número de processo já existe na base de dados!. N.º Processo " + processos.NumeroProcesso);
            }
            else
            {
                _context.Processos.Add(processos);
                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateException)
                {
                    if (ProcessosExists(processos.NumeroProcesso))
                    {
                        return Conflict();
                    }
                    else
                    {
                        throw;
                    }
                }

                return CreatedAtAction("GetProcessos", new { id = processos.NumeroProcesso }, processos);
            }

        }

        // DELETE: api/Processos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Processos>> DeleteProcessos(string id)
        {
            var processos = await _context.Processos.FindAsync(id);
            if (processos == null)
            {
                return NotFound();
            }

            var movimentacaoProcesso = _context.ProcessoMovimentacoes
                                        .Where(n => n.NumeroProcesso == id)
                                        .FirstOrDefault();
            if (movimentacaoProcesso != null)
                return BadRequest("Exclusão não permitida. Processo possui movimentação!");

            _context.Processos.Remove(processos);
            await _context.SaveChangesAsync();

            return processos;
        }

        private bool ProcessosExists(string id)
        {
            return _context.Processos.Any(e => e.NumeroProcesso == id);
        }
    }
}
