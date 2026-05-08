using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ViziLogin.Data;
using ViziLogin.Models;

namespace ViziLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AppDbContext _context;

        public HomeController(ILogger<HomeController> logger, AppDbContext context)
        {
            _logger = logger;
            _context = context;
        }


public async Task<IActionResult> Index(string searchString, string tipo, int pagina = 1)
        {
            // 3. Começamos com um IQueryable. Nada foi buscado do banco ainda.
            IQueryable<Servico> consulta = _context.Servicos;

            // 4. Filtro de busca por texto
            if (!string.IsNullOrEmpty(searchString))
            {
                // O EF Core transformará isso em um comando SQL "WHERE ... LIKE"
                consulta = consulta.Where(s =>
                    s.NomeServico.Contains(searchString) ||
                    s.Profissional.Contains(searchString) ||
                    s.TipoServico.Contains(searchString) ||
                    s.Preco.Contains(searchString) ||
                    s.Contato.Contains(searchString)
                );
            }

            // 5. Filtro por Tipo de Serviço
            if (!string.IsNullOrWhiteSpace(tipo) && !tipo.Equals("Todos", StringComparison.OrdinalIgnoreCase))
            {
                consulta = consulta.Where(s => s.TipoServico == tipo);
            }

            // 6. Lógica de Paginação (agora consultando o banco)
            int itensPorPagina = 8;
            int totalItens = await consulta.CountAsync(); // Conta o total no banco
            int totalPaginas = (int)Math.Ceiling((double)totalItens / itensPorPagina);

            // Garante que a página não seja menor que 1
            if (pagina < 1) pagina = 1;

            var servicosPaginados = await consulta
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToListAsync(); // Aqui a consulta é executada no banco

            // 7. ViewBags para manter o estado na interface
            ViewBag.PaginaAtual = pagina;
            ViewBag.TotalPaginas = totalPaginas;
            ViewBag.SearchString = searchString;
            ViewBag.TipoSelecionado = string.IsNullOrEmpty(tipo) ? "Todos" : tipo;

            return View(servicosPaginados);
        }





public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}


