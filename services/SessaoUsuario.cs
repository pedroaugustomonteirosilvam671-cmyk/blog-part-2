using BLOGGGG.Model;

namespace BLOGGGG.Service;

public class SessaoUsuario
{
    public Usuario? UsuarioAtual {get; private set;}
    public bool EstaAutenticado => UsuarioAtual != null;
    public int? UsuarioId => UsuarioAtual?.Id;

    public void IniciarSessao(Usuario usuario)
    {
       UsuarioAtual = usuario;
    }

    public void EncerrarSessao()
    {
       UsuarioAtual = null;
    }
}