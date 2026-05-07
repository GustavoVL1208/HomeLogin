using ViziLogin.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ViziLogin.Controllers
{
    public class LoginController : Controller
    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string senha)
        {

            var user = _context.Usuarios
                .FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (user == null)
            {
                ViewBag.Error = "Dados inválidos ou incorretos";
                return View();
            }

            TempData["Success"] = "Transação OK, você está logado";

            return RedirectToAction("Index", "Home");
        }
    }
}