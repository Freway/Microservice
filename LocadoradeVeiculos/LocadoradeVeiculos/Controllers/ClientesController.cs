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
    public class ClientesController : ControllerBase
    {
        private readonly LocadoraContext _context;
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(LocadoraContext context, ILogger<ClientesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cliente>>> GetClientes()
        {
            _logger.LogInformation("Sem erro, Get trazendo resposta");
            return await _context.Clientes.ToListAsync();
        }

        // GET: api/Clientes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cliente>> GetCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);

            if (cliente == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _logger.LogInformation("GET {ID} da consulta", id);
            return cliente;
        }

        // PUT: api/Clientes/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCliente(int id, Cliente cliente)
        {
            if (id != cliente.IdCliente)
            {
                _logger.LogInformation("400 - Bad Request");
                return BadRequest();
            }

            _context.Entry(cliente).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Atualizado {NomeCliente}", cliente.NomeCliente);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClienteExists(id))
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

        // POST: api/Clientes
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cliente>> PostCliente(Cliente cliente)
        {
            //if (ValidaCPF.IsCpf(cliente.CPF))
            //{
                _context.Clientes.Add(cliente);
                await _context.SaveChangesAsync();                               
            //}
            _logger.LogInformation("Adicionado novo cliente");
            return CreatedAtAction(nameof(GetCliente), new { id = cliente.IdCliente }, cliente);
        }

        // DELETE: api/Clientes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cliente>> DeleteCliente(int id)
        {
            var cliente = await _context.Clientes.FindAsync(id);
            if (cliente == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _context.Clientes.Remove(cliente);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Removido Cliente {NomeCliente}", cliente.NomeCliente);
            return cliente;
        }

        private bool ClienteExists(int id)
        {
            return _context.Clientes.Any(e => e.IdCliente == id);
        }
    }
}
