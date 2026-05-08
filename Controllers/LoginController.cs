using ViziLogin.Data;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Threading.Tasks;
using System.Collections.Generic; // Necessário para o List<Claim>

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
        public async Task<IActionResult> Index(string email, string senha)
        {
            // O código precisa estar AQUI DENTRO
            var user = _context.Usuarios
                .FirstOrDefault(x => x.Email == email && x.Senha == senha);

            if (user == null)
            {
                ViewBag.Error = "Dados inválidos ou incorretos";
                return View();
            }

            // Agora o 'var' e o 'await' vão funcionar porque estão dentro de um método async
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Tipo_Perfil ?? "")
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity)
            );

            TempData["Success"] = "Transação OK, você está logado";

            return RedirectToAction("Index", "Home");
        }
    }
}