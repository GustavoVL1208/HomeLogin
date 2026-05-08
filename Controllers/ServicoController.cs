using Microsoft.AspNetCore.Mvc;
using ViziLogin.Data;
using ViziLogin.Models;

namespace ViziLogin.Controllers
{
    public class ServicoController : Controller
    {
        private readonly AppDbContext _context;

        public ServicoController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult NovoServico()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Servico servico)
        {
            if (!ModelState.IsValid)
            {
                return View("NovoServico", servico);
            }

            _context.Servicos.Add(servico);
            _context.SaveChanges();

            TempData["Sucesso"] = "Serviço criado com sucesso!";

            return RedirectToAction("Index", "Home");
        }
    }
}

