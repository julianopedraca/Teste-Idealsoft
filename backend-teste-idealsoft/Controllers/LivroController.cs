using backend_teste_idealsoft.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace backend_teste_idealsoft.Controllers
{
    [ApiController]
    [Route("livros")]
    public class LivroController : ControllerBase
    {
        private readonly LivrosContext _context;
        public LivroController(LivrosContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            var livros = await _context.Livros.ToListAsync();

            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ListarPorId(int id)
        {
            var livro = await _context.Livros.FirstOrDefaultAsync(l => l.Id == id);
            if (livro == null)
            {
                return NotFound(); 
            };
            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] Livro livro)
        {
            _context.Add(livro);
            await _context.SaveChangesAsync();

            return Ok("livro adicionado");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar([FromBody] Livro livroAtualizado)
        {

            _context.Update(livroAtualizado);
            await _context.SaveChangesAsync();

            return Ok("Livro Atualizado"); // Retorna um status 204 para indicar que a atualização foi bem-sucedida.
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id)
        {
            var livro = await _context.Livros.FindAsync(id);

            _context.Livros.Remove(livro);
            await _context.SaveChangesAsync();

            return Ok("livro Excluido"); 
        }
    }
}