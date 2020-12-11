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
using System.Net.Http;

namespace LocadoradeVeiculos.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstoquesController : ControllerBase
    {
        private readonly LocadoraContext _context;
        private readonly ILogger<EstoquesController> _logger;
        public EstoquesController(LocadoraContext context, ILogger<EstoquesController> logger)
        {
            _logger = logger;
            _context = context;
        }

        // GET: api/Estoques
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estoque>>> GetEstoques()
        {
            _logger.LogInformation("Sem erro, Get trazendo resposta");
            return await _context.Estoques.ToListAsync();
        }

        // GET: api/Estoques/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Estoque>> GetEstoque(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);

            if (estoque == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _logger.LogInformation("GET {ID} da consulta", id);
            return estoque;
        }

        // PUT: api/Estoques/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEstoque(int id, Estoque estoque)
        {
            if (id != estoque.IdEstoque)
            {
                _logger.LogInformation("400 - Bad Request");
                return BadRequest();
            }

            _context.Entry(estoque).State = EntityState.Modified;

            try
            {
                _logger.LogInformation("Atualizado {ID}", id);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EstoqueExists(id))
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

        // POST: api/Estoques
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Estoque>> PostEstoque(Estoque estoque)
        {          
            try
            {
                _context.Estoques.Add(estoque);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                _logger.LogInformation("Placa ou id já cadastrado {Placa}", estoque.Placa);
                return BadRequest("Placa ou id já cadastrado");
            }

            _logger.LogInformation("Cadastrado novo veiculo");
            return CreatedAtAction(nameof(GetEstoque), new { id = estoque.IdEstoque }, estoque);
        }

        // DELETE: api/Estoques/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Estoque>> DeleteEstoque(int id)
        {
            var estoque = await _context.Estoques.FindAsync(id);
            if (estoque == null)
            {
                _logger.LogInformation("404 - Not Found");
                return NotFound();
            }

            _context.Estoques.Remove(estoque);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Deletado veiculo {ID}", id);
            return estoque;
        }

        //private void FipeMarcas()
        //{
        //    List<String> tipo = new List<string>();
        //    tipo.Add("Carros");
        //    tipo.Add("Motos");
        //    tipo.Add("Caminhoes");

        //    var URLMarcas = "http://fipeapi.appspot.com/api/1/" + tipo + "/marcas.json";

        //    using (var client = new HttpClient())
        //    {
        //        using (var response = await client.GetAsync(URLMarcas))
        //        {
        //            if (response.IsSuccessStatusCode)
        //            {
        //                var ProdutoJsonString = await response.Content.ReadAsStringAsync();
        //                dgvDados.DataSource = JsonConvert.DeserializeObject<Produto[]>(ProdutoJsonString).ToList();
        //            }
        //            else
        //            {
        //                _logger.LogInformation("204 - No Content");
        //                return NoContent();
        //            }
        //        }
        //    }
        //}
        private bool EstoqueExists(int id)
        {
            return _context.Estoques.Any(e => e.IdEstoque == id);
        }

        //static void BuscaEstoque(IQueryable<int> items)
        //{
        //    Console.WriteLine($"Average: {items.Average()}");
        //    Console.WriteLine($"Sum: {items.Sum()}");
        //}
    }
}
