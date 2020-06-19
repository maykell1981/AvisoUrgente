using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
    public class ProcessoMovimentacoesController : ControllerBase
    {
        private readonly AvisoUrgenteContext _context;

        public ProcessoMovimentacoesController(AvisoUrgenteContext context)
        {
            _context = context;
        }

        // GET: api/ProcessoMovimentacoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProcessoMovimentacoes>>> GetProcessoMovimentacoes()
        {
            return await _context.ProcessoMovimentacoes.ToListAsync();
        }

        // GET: api/ProcessoMovimentacoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ProcessoMovimentacoes>> GetProcessoMovimentacoes(int id)
        {
            var processoMovimentacoes = await _context.ProcessoMovimentacoes.FindAsync(id);

            if (processoMovimentacoes == null)
            {
                return NotFound();
            }

            return processoMovimentacoes;
        }


        [HttpGet("BuscaProcessoMovimentcoesAplicacao/{numeroProcesso}/{data}/{descricao}")]
        public async Task<ActionResult<ProcessoMovimentacoes>> BuscaProcessoMovimentcoesAplicacao(string numeroProcesso, string data, string descricao)
        {
            data = data.Replace("-", "/");
            var processoMovimentacoes = _context.ProcessoMovimentacoes
                                            .Where(n => n.Data == data
                                                    && n.Descricao == descricao
                                                    && n.NumeroProcesso == numeroProcesso)
                                            .Select(n => n.ProcessoMovimentacaoId)
                                            .FirstOrDefault();

            if (processoMovimentacoes == 0)
            {
                return Ok();
            }

            return NotFound();
        }

        // PUT: api/ProcessoMovimentacoes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProcessoMovimentacoes(int id, ProcessoMovimentacoes processoMovimentacoes)
        {
            var formato = "dd/MM/yyyy";
            DateTime dataMovimentacao;
            var dataMovimentacaoConvertida = DateTime.TryParseExact(processoMovimentacoes.Data, formato, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dataMovimentacao);

            if (id != processoMovimentacoes.ProcessoMovimentacaoId)
                return BadRequest("Registro não encontrado!");

            var NumeroProcesso = _context.Processos
                                    .Where(n => n.NumeroProcesso == processoMovimentacoes.NumeroProcesso)
                                    .Select(n => n.NumeroProcesso)
                                    .FirstOrDefault();

            if (!dataMovimentacaoConvertida)
                return BadRequest("Data da movimentação iválida!");

            if (processoMovimentacoes.Data == null)
                return BadRequest("Necessário informar a data da movimentação!");

            if (processoMovimentacoes.Descricao == null)
                return BadRequest("Necessário informar a descrição da movimentação!");

            if (NumeroProcesso == null)
                return BadRequest("Número de processo não existe na base de dados!");

            _context.Entry(processoMovimentacoes).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProcessoMovimentacoesExists(id))
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

        // POST: api/ProcessoMovimentacoes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<ProcessoMovimentacoes>> PostProcessoMovimentacoes(ProcessoMovimentacoes processoMovimentacoes)
        {
            var cultureInfo = new CultureInfo("pt-BR");

            var formato = "dd/MM/yyyy";
            DateTime dataMovimentacao;
            var dataMovimentacaoConvertida = DateTime.TryParseExact(processoMovimentacoes.Data, formato, CultureInfo.InvariantCulture, DateTimeStyles.AllowWhiteSpaces, out dataMovimentacao);

            var NumeroProcesso = _context.Processos
                                    .Where(n => n.NumeroProcesso == processoMovimentacoes.NumeroProcesso)
                                    .Select(n => n.NumeroProcesso)
                                    .FirstOrDefault();

            if (!dataMovimentacaoConvertida)
                return BadRequest("Data da movimentação iválida!");

            if (processoMovimentacoes.Data == null)
                return BadRequest("Necessário informar a data da movimentação!");

            if (processoMovimentacoes.Descricao == null)
                return BadRequest("Necessário informar a descrição da movimentação!");

            if (NumeroProcesso == null)
                return BadRequest("Número de processo não existe na base de dados!");

            _context.ProcessoMovimentacoes.Add(processoMovimentacoes);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProcessoMovimentacoes", new { id = processoMovimentacoes.ProcessoMovimentacaoId }, processoMovimentacoes);

        }

        // DELETE: api/ProcessoMovimentacoes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ProcessoMovimentacoes>> DeleteProcessoMovimentacoes(int id)
        {
            var processoMovimentacoes = await _context.ProcessoMovimentacoes.FindAsync(id);
            if (processoMovimentacoes == null)
            {
                return NotFound();
            }

            _context.ProcessoMovimentacoes.Remove(processoMovimentacoes);
            await _context.SaveChangesAsync();

            return processoMovimentacoes;
        }

        private bool ProcessoMovimentacoesExists(int id)
        {
            return _context.ProcessoMovimentacoes.Any(e => e.ProcessoMovimentacaoId == id);
        }
    }
}
