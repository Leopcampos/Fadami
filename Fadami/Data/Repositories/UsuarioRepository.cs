using Fadami.Data;
using Fadami.Models;
using Microsoft.EntityFrameworkCore;

namespace Fadami.Data.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly SqlContext _context;

        public UsuarioRepository(SqlContext context)
        {
            _context = context;
        }

        public Usuario BuscarPorLogin(string login)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Login == login);
        }

        public List<Usuario> BuscarTodos()
        {
            return _context.Usuarios.ToList();
        }

        public Usuario BuscarPorId(int id)
        {
            return _context.Usuarios.FirstOrDefault(x => x.Codigo == id);
        }

        public Usuario Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            _context.SaveChanges();
            return usuario;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            Usuario usuarioDB = BuscarPorId(usuario.Codigo);

            if (usuarioDB == null) throw new Exception("Houve um erro na atualização do usuário");

            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Login = usuario.Login;
            usuarioDB.CPF = usuario.CPF;
            usuarioDB.Senha = usuario.Senha;
            usuarioDB.UltimoAcesso = DateTime.Now;
            usuarioDB.QtdErroLogin = usuario.QtdErroLogin;
            usuarioDB.BLAtivo = usuario.BLAtivo;


            _context.Usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            Usuario usuarioDB = BuscarPorId(id);

            if (usuarioDB == null) throw new Exception("Houve um erro na deleção do usuário");

            _context.Usuarios.Remove(usuarioDB);
            _context.SaveChanges();

            return true;
        }

        public void AtualizarQtdErroLogin(Usuario usuario)
        {
            var usuarioDB = _context.Usuarios.FirstOrDefault(u => u.Codigo == usuario.Codigo);
            usuarioDB.QtdErroLogin = usuario.QtdErroLogin;
            _context.SaveChanges();
        }

        public void AtualizarBlAtivo(Usuario usuario)
        {
            var usuarioDB = _context.Usuarios.FirstOrDefault(u => u.Codigo == usuario.Codigo);
            usuarioDB.BLAtivo = usuario.BLAtivo;
            _context.SaveChanges();
        }

        public void AtualizarUsuario(Usuario usuario)
        {
            var existingUser = _context.Usuarios.Find(usuario.Codigo);

            if (existingUser != null)
            {
                existingUser.Nome = usuario.Nome;
                existingUser.Login = usuario.Login;
                existingUser.Senha = usuario.Senha;
                existingUser.QtdErroLogin = usuario.QtdErroLogin;
                existingUser.BLAtivo = usuario.BLAtivo;
                existingUser.UltimoAcesso = usuario.UltimoAcesso;
                _context.SaveChanges();
            }
        }
    }
}