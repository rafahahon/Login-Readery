using Microsoft.AspNetCore.Mvc;
using Login_Readery.Data;
using Login_Readery.Services;

namespace Login_Readery.Controllers
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
        public IActionResult Entrar(string email, string senha)
        {
            // IsNullOrWhiteSpace - verifica se existe espaços em branco ou se está vazio
            if(string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            {
                ViewBag.Erro = "Preencha todos os campos.";
                return View("Index");
            }

            // hash da senha digitada
            byte[] senhaDigitadaHash = HashService.GerarHashBytes(senha);

            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);

            if(usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            if(!usuario.SenhaHash.SequenceEqual(senhaDigitadaHash))
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);

            return RedirectToAction("Index", "Home"); // substituir HOME por LIVRO
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}