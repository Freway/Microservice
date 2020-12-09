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
    public class FuncionariosController : ControllerBase
    {
        private readonly LocadoraContext _context;
        private readonly ILogger<EstoquesController> _logger;
        public FuncionariosController(LocadoraContext context, ILogger<EstoquesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Funcionarios
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> GetFuncionarios()
        {
            _logger.LogInformation("Sem erro, Get trazendo resposta");
            return await _context.Funcionarios.ToListAsync();
        }

        // GET: api/Funcionarios/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _logger.LogInformation("GET {ID} da consulta", id);
            return funcionario;
        }

        // PUT: api/Funcionarios/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFuncionario(int id, Funcionario funcionario)
        {
            if (id != funcionario.IdFuncionario)
            {
                _logger.LogInformation("400 - Bad Request");
                return BadRequest();
            }

            _context.Entry(funcionario).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Atualizado {Nome}", funcionario.Nome);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FuncionarioExists(id))
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

        // POST: api/Funcionarios
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Cadastrado novo funcionario");
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.IdFuncionario }, funcionario);
        }

        // DELETE: api/Funcionarios/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Funcionario>> DeleteFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }
                       
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deletado funcionario {Nome}", funcionario.Nome);
            return funcionario;
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.IdFuncionario == id);
        }
    }
}
