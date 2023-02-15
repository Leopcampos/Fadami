using Fadami.Data.Repositories;
using Fadami.Helper;
using Fadami.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace Fadami.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsuarioRepository _usuarioReposity;

        private readonly ISessao _sessao;

        public LoginController(IUsuarioRepository usuarioReposity, ISessao sessao)
        {
            _usuarioReposity = usuarioReposity;
            _sessao = sessao;
        }

        public IActionResult Index()
        {
            // Se o usuário estiver logado, redirecionar para Home
            if (_sessao.BuscarSessaoDoUsuario() != null)
                return RedirectToAction("Index", "Home");

            return View();
        }


        public IActionResult Sair()
        {
            // Obtém o usuário atualmente logado
            Usuario usuario = _sessao.BuscarSessaoDoUsuario();

            // Remove a sessão do usuário anterior
            _sessao.RemoverSessaoDoUsuario();
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Login");
        }


        [HttpPost]
        public IActionResult Entrar(LoginModel loginModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuarioReposity.BuscarPorLogin(loginModel.Login);

                    if (usuario != null)
                    {
                        if (usuario.SenhaValida(loginModel.Senha))
                        {
                            if (usuario.BLAtivo == false)
                            {
                                TempData["MensagemErro"] = "Usuário bloqueado. Contate o administrador do sistema para mais informações.";
                                return RedirectToAction("Index");
                            }

                            usuario.QtdErroLogin = 0;
                            usuario.UltimoAcesso = DateTime.Now;
                            _usuarioReposity.AtualizarUsuario(usuario);
                            _sessao.CriarSessaoDoUsuario(usuario);

                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha do usuário é inválida";

                        if (usuario.QtdErroLogin >= 3)
                        {
                            usuario.BLAtivo = false;
                            _usuarioReposity.AtualizarUsuario(usuario);

                            TempData["MensagemErro"] = $"Usuário bloqueado após 3 tentativas de login falhas";
                            return RedirectToAction("Index");
                        }

                        usuario.QtdErroLogin++;
                        _usuarioReposity.AtualizarUsuario(usuario);
                    }

                    TempData["MensagemErro"] = $"Usuário e/ou inválido";
                }

                return View("Index");
            }
            catch (Exception erro)
            {
                TempData["MensagemErro"] = $"Ops, não conseguimos realizar seu login, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [HttpPost]
        public IActionResult CriarNovoLogin(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _usuarioReposity.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index", "Login");
                }
                return View(usuario);
            }
            catch (Exception erro)
            {

                TempData["MensagemErro"] = $"Ops, não conseguimos cadastrar o usuário, tente novamente, detalhe do erro:{erro.Message}";
                return RedirectToAction("Index", "Login");
            }
        }
    }
}