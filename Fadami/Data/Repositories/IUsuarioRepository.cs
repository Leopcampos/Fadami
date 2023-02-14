
using Fadami.Models;
using System.Collections.Generic;

namespace Fadami.Data.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario BuscarPorLogin(string login);
        List<Usuario> BuscarTodos();
        Usuario BuscarPorId(int id);
        Usuario Adicionar(Usuario usuario);
        Usuario Atualizar(Usuario usuario);
        bool Apagar(int id);
        void AtualizarQtdErroLogin(Usuario usuario);
        void AtualizarBlAtivo(Usuario usuario);
        void AtualizarUsuario(Usuario usuario);
    }
}