using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using ViziLogin.Models;

namespace ViziLogin.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }



        public IActionResult Index(string searchString, string tipo, int pagina = 1)
        {

            var todosServicos = new List<Servico>
    {
new Servico { Titulo = "Eletricista 24h", Nome = "Marcos Oliveira", TipoServico = "Presencial", Contato = "31 98877-6655", Preco = "R$ 100-250" },
new Servico { Titulo = "Aula de Inglês", Nome = "Beatriz Rocha", TipoServico = "Online", Contato = "31 97766-5544", Preco = "R$ 60/hora" },
new Servico { Titulo = "Personal Trainer", Nome = "Ricardo Nunes", TipoServico = "Presencial", Contato = "31 96655-4433", Preco = "R$ 90/sessão" },
new Servico { Titulo = "Desenvolvedor Web", Nome = "Lucas Mendes", TipoServico = "Online", Contato = "31 95544-3322", Preco = "Sob Consulta" },
new Servico { Titulo = "Montador de Móveis", Nome = "José Alencar", TipoServico = "Presencial", Contato = "31 94433-2211", Preco = "R$ 50-180" },
new Servico { Titulo = "Psicóloga Clínica", Nome = "Mariana Costa", TipoServico = "Online", Contato = "31 93322-1100", Preco = "R$ 120/sessão" },
new Servico { Titulo = "Limpeza de Estofados", Nome = "Sérgio Lima", TipoServico = "Presencial", Contato = "31 92211-0099", Preco = "R$ 150-300" },
new Servico { Titulo = "Designer Gráfico", Nome = "Aline Souza", TipoServico = "Online", Contato = "31 91100-9988", Preco = "R$ 70/hora" },
new Servico { Titulo = "Mecânico a Domicílio", Nome = "Roberto Dias", TipoServico = "Presencial", Contato = "31 90099-8877", Preco = "R$ 120-400" },
new Servico { Titulo = "Aulas de Violão", Nome = "Fernando Braga", TipoServico = "Online", Contato = "31 99988-7766", Preco = "R$ 55/aula" },
new Servico { Titulo = "Jardineiro", Nome = "Wilson Duarte", TipoServico = "Presencial", Contato = "31 98877-6644", Preco = "R$ 80-200" },
new Servico { Titulo = "Tradução de Textos", Nome = "Carla Vieira", TipoServico = "Online", Contato = "31 97766-5533", Preco = "R$ 0,15/palavra" },
new Servico { Titulo = "Manicure e Pedicure", Nome = "Juliana Neves", TipoServico = "Presencial", Contato = "31 96655-4422", Preco = "R$ 40-70" },
new Servico { Titulo = "Suporte de TI", Nome = "Thiago Santos", TipoServico = "Online", Contato = "31 95544-3311", Preco = "R$ 80/chamado" },
new Servico { Titulo = "Pedreiro Reformas", Nome = "Gilberto Silva", TipoServico = "Presencial", Contato = "31 94433-2200", Preco = "Diária R$ 180" },
new Servico { Titulo = "Marketing Digital", Nome = "Patrícia Gomes", TipoServico = "Online", Contato = "31 93322-1199", Preco = "R$ 200-800" },
new Servico { Titulo = "Dog Walker", Nome = "Gabriel Faria", TipoServico = "Presencial", Contato = "31 92211-0088", Preco = "R$ 30/passeio" }
    };

            if (!string.IsNullOrEmpty(searchString))
            {

                todosServicos = todosServicos.Where(s =>
                        (s.Titulo != null && s.Titulo.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                        (s.Nome != null && s.Nome.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                        (s.TipoServico != null && s.TipoServico.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                        (s.Preco != null && s.Preco.Contains(searchString, StringComparison.OrdinalIgnoreCase)) ||
                        (s.Contato != null && s.Contato.Contains(searchString, StringComparison.OrdinalIgnoreCase))
                    ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(tipo) && !tipo.Equals("Todos", StringComparison.OrdinalIgnoreCase))
                {
                    todosServicos = todosServicos
                        .Where(s => s.TipoServico != null && s.TipoServico.Trim().Equals(tipo.Trim(), StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
            

            int itensPorPagina = 8;
            int totalItens = todosServicos.Count;
            int totalPaginas = (int)Math.Ceiling((double)totalItens / itensPorPagina);

            var servicosPaginados = todosServicos
                .Skip((pagina - 1) * itensPorPagina)
                .Take(itensPorPagina)
                .ToList();



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
