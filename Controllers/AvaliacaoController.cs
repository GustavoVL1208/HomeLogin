using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ViziLogin.Data;
using ViziLogin.Models;

namespace ViziLogin.Controllers
{
    public class AvaliacaoController : Controller
    {
        private readonly AppDbContext _context;

        public AvaliacaoController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var avaliacoes = await _context.Avaliacao
                .OrderByDescending(a => a.DataAvaliacao)
                .ToListAsync();
            return View(avaliacoes);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NomeAvaliador,Nota,Comentario,ServicoId")] Avaliacao avaliacao)
        {
            if (ModelState.IsValid)
            {
                avaliacao.DataAvaliacao = DateTime.Now;
                _context.Add(avaliacao);
                await _context.SaveChangesAsync();

                return Redirect(Request.Headers["Referer"].ToString());
            }
            return BadRequest("Dados inválidos");
        }
    }
}
