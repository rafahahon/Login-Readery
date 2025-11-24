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

            // buscar usuário pelo email
            // FirstOrDefault -> procura o usuário pelo email, se não encontrar retorna null
            var usuario = _context.Usuarios.FirstOrDefault(usuario => usuario.Email == email);

            if(usuario == null)
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            // comparar byte a byte da senha
            // SequenceEqual -> retorna false se qualquer byte estiver diferente
            if(!usuario.SenhaHash.SequenceEqual(senhaDigitadaHash))
            {
                ViewBag.Erro = "E-mail ou senha incorretos.";
                return View("Index");
            }

            // Login estiver OK -> salva na sessão
            HttpContext.Session.SetString("UsuarioNome", usuario.Nome);
            HttpContext.Session.SetInt32("UsuarioId", usuario.IdUsuario);

            // retorna para a index da home, não da login
            return RedirectToAction("Index", "Home");
        }

        public IActionResult Sair()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}