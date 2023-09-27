using backend_teste_idealsoft.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend_teste_idealsoft.Controllers
{
    [ApiController]
    [Route("agenda")]
    public class AgendaController : ControllerBase
    {
        private readonly AgendaContext _context;
        public AgendaController(AgendaContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var agenda = await _context.Agendas.ToListAsync();

            return Ok(agenda);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var livro = await _context.Agendas.FirstOrDefaultAsync(l => l.Id == id);
            if (livro == null)
            {
                return NotFound(); 
            };
            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Agenda agenda)
        {
            _context.Add(agenda);
            await _context.SaveChangesAsync();

            return Ok("contato adicionado");
        }

        [HttpPut]
        public async Task<IActionResult> Atualizar([FromBody] Agenda contatoAtualizado)
        {
            _context.Update(contatoAtualizado);
            await _context.SaveChangesAsync();

            return Ok("contato Atualizado"); 
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var livro = await _context.Agendas.FindAsync(id);

            _context.Agendas.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok("contato Excluido"); 
        }
    }
}