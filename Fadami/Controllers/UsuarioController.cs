using Fadami.Data;
using Fadami.Data.Repositories;
using Fadami.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Fadami.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly SqlContext _context;

        public UsuarioController(IUsuarioRepository usuarioRepository, SqlContext context)
        {
            _usuarioRepository = usuarioRepository;
            _context = context;
        }

        public IActionResult Index()
        {
            List<Usuario> usuarios = _usuarioRepository.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult BuscarTodos()
        {
            List<Usuario> usuarios = _usuarioRepository.BuscarTodos();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioRepository.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuário, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Criar");
            }
        }


        public IActionResult Editar(int id)
        {
            Usuario usuario = _usuarioRepository.BuscarPorId(id);
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Editar(UsuarioSemSenhaModel usuarioSemSenhaModel)
        {
            try
            {
                Usuario usuario = null;

                if (ModelState.IsValid)
                {
                    usuario = new Usuario()
                    {
                        Codigo = usuarioSemSenhaModel.Codigo,
                        Nome = usuarioSemSenhaModel.Nome,
                        Login = usuarioSemSenhaModel.Login,
                        CPF = usuarioSemSenhaModel.CPF,
                        Senha = usuarioSemSenhaModel.Senha,
                        BLAtivo = usuarioSemSenhaModel.BLAtivo
                    };

                    usuario = _usuarioRepository.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário alterado com sucesso!";
                    return RedirectToAction("Index");
                }

                return View(usuario);
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos atualizar seu usuário, tente novamante, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            Usuario usuario1 = _usuarioRepository.BuscarPorId(id);
            return View(usuario1);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepository.Apagar(id);

                if (apagado) TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
                else TempData["MensagemErro"] = "Ops, não conseguimos apagar o usuário";

                return RedirectToAction("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos apagar o usuário, mais detalhes do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public async Task<IActionResult> Login(Usuario usuario)
        {
            Usuario usuarioBD = await _context.Usuarios.
                FirstOrDefaultAsync(u => u.Login == usuario.Login && u.Senha == usuario.Senha);

            if (usuarioBD == null || usuarioBD.Senha != usuario.Senha)
            {
                if (usuarioBD != null)
                {
                    usuarioBD.QtdErroLogin++;
                    if (usuarioBD.QtdErroLogin >= 3)
                    {
                        usuarioBD.BLAtivo = false;
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction("Home", "Index");
            }

            usuarioBD.UltimoAcesso = DateTime.Now;
            usuarioBD.QtdErroLogin = 0;
            await _context.SaveChangesAsync();

            return RedirectToAction("ListarUsuarios", "Usuario");
        }
    }
}