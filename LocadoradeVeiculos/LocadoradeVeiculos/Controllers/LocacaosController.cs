using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LocadoradeVeiculos.Data;
using LocadoradeVeiculos.Models;
using Microsoft.Extensions.Logging;

namespace LocadoradeVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocacaosController : ControllerBase
    {
        private readonly LocadoraContext _context;
        private readonly ILogger<EstoquesController> _logger;
        public LocacaosController(LocadoraContext context,ILogger<EstoquesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Locacaos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Locacao>>> GetLocacoes()
        {
            _logger.LogInformation("Sem erro, Get trazendo resposta");
            return await _context.Locacoes.ToListAsync();
        }

        // GET: api/Locacaos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Locacao>> GetLocacao(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);

            if (locacao == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _logger.LogInformation("GET {ID} da consulta", id);
            return locacao;
        }

        // PUT: api/Locacaos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLocacao(int id, Locacao locacao)
        {
            if (id != locacao.IdLocacao)
            {
                _logger.LogInformation("400 - Bad Request");
                return BadRequest();
            }

            _context.Entry(locacao).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Atualizado {ID}", id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LocacaoExists(id))
                {
                    _logger.LogInformation("404 - Not Found");
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            _logger.LogInformation("204 - No Content");
            return NoContent();
        }

        // POST: api/Locacaos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Locacao>> PostLocacao(Locacao locacao)
        {
            _context.Locacoes.Add(locacao);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cadastrado nova locacao");
            return CreatedAtAction(nameof(GetLocacao), new { id = locacao.IdLocacao }, locacao);
        }

        // DELETE: api/Locacaos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Locacao>> DeleteLocacao(int id)
        {
            var locacao = await _context.Locacoes.FindAsync(id);
            if (locacao == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _context.Locacoes.Remove(locacao);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deletado {ID}", id);
            return locacao;
        }

        private bool LocacaoExists(int id)
        {
            return _context.Locacoes.Any(e => e.IdLocacao == id);
        }
    }
}
