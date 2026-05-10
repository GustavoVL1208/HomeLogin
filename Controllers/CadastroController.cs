using Microsoft.AspNetCore.Mvc;
using ViziLogin.Data;
using ViziLogin.Models;
using Microsoft.EntityFrameworkCore;

public class CadastroController : Controller
{
    private readonly AppDbContext _context;

    public CadastroController(AppDbContext context)
    {
        _context = context;
    }

    // Carrega a página (GET)
    public IActionResult Index()
    {
        return View();
    }

    // Processa o formulário (POST)
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Cadastrar(Usuario usuario, string ConfirmarSenha)
    {
        // 1. Validação manual de senha
        if (usuario.Senha != ConfirmarSenha)
        {
            ViewBag.Error = "As senhas não coincidem!";
            return View("Index", usuario);
        }

        // 2. Limpar erros de validação automáticos para campos que não estão no form
        // Isso garante que o código entre no bloco de salvar
        ModelState.Remove("Id");
        ModelState.Remove("Id_Area");

        if (ModelState.IsValid)
        {
            // 3. Verificar duplicidade
            var usuarioExiste = await _context.Usuarios.AnyAsync(u => u.Email == usuario.Email);
            if (usuarioExiste)
            {
                ViewBag.Error = "Este e-mail já está cadastrado.";
                return View("Index", usuario);
            }

            // 4. Definir Id_Area como null (ou um valor padrão) explicitamente
            usuario.Id_Area = null;

            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Login");
        }

        // Se chegar aqui, algo ainda está errado. Vamos mostrar o erro no console:
        var erros = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
        ViewBag.Error = "Erro nos dados: " + string.Join(", ", erros);

        return View("Index", usuario);
    }
}