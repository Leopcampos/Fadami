using Fadami.Models;

namespace Fadami.Helper
{
    public interface ISessao
    {
        void CriarSessaoDoUsuario(Usuario usuario);
        void RemoverSessaoDoUsuario();
        Usuario BuscarSessaoDoUsuario();
    }
}