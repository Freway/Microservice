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
        [Route("Estoque")]
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
            ////string jsonData = "";
            //var evento = new RootObject();
            ////jsonData = JsonConvert.SerializeObject(_getEventos);
            //evento.identificador = "POINTER";
            //evento.eventos = listaEventos;

            //var client = new RestClient(ConfigurationManager.AppSettings["url"]);
            //var request = new RestRequest(ConfigurationManager.AppSettings["path"], Method.POST);
            ////var request = new RestRequest(Method.POST);
            //request.RequestFormat = DataFormat.Json;
            //request.AddHeader("content-Type", "application/json");
            ////request.AddParameter("application/json", jsonData, ParameterType.RequestBody);
            //request.AddParameter("Token", _Token, ParameterType.QueryString);
            //request.AddParameter("DataHora", DateTime.Now.ToString(_formatData), ParameterType.QueryString);
            //request.AddBody(evento);

            //var retorno = client.Execute(request);

            _context.Estoques.Add(estoque);
            await _context.SaveChangesAsync();

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

            _logger.LogInformation("Deletado veiculo {ID}",id);
            return estoque;
        }

        private bool EstoqueExists(int id)
        {
            return _context.Estoques.Any(e => e.IdEstoque == id);
        }
    }
}
