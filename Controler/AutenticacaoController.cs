using System;
using System.Threading.Tasks;
using BLOGGGG.Model;
using BLOGGGG.Service;
using BLOGGGG.View;

namespace BLOGGGG.Controller;

public class AutenticacaoController
{
    private readonly UsuarioService _usuarioService;
    private readonly AutenticacaoView _autenticacaoView;
    private readonly SessaoUsuario _sessaoUsuario;

    public AutenticacaoController(
        UsuarioService usuarioService, 
        AutenticacaoView autenticacaoView, 
        SessaoUsuario sessaoUsuario)
    {
        _usuarioService = usuarioService;
        _autenticacaoView = autenticacaoView;
        _sessaoUsuario = sessaoUsuario;
    }

    public async Task CadastrarAsync()
    {
        Usuario dados = _autenticacaoView.SolicitarDadosCadastro();

        var (sucesso, mensagem, novoUsuario) = await _usuarioService.CadastrarAsync(
            dados.Nome,
            dados.Email,
            dados.Senha
        );

        _autenticacaoView.ExibirMensagem(mensagem);

        if (sucesso && novoUsuario != null)
        {
            _sessaoUsuario.IniciarSessao(novoUsuario);
        }
    }

    public async Task AutenticarAsync()
    {

        var (email, senha) = _autenticacaoView.SolicitarCredenciais();

        var (sucesso, mensagem, usuario) = await _usuarioService.AutenticarAsync(email, senha);

        _autenticacaoView.ExibirMensagem(mensagem);

        if (sucesso && usuario != null)
        {
            _sessaoUsuario.IniciarSessao(usuario);
        }
    }
    public void Sair()
    {
        if (_sessaoUsuario.EstaAutenticado)
        {
           _autenticacaoView.ExibirMensagem("Sessão encerrada com sucesso.");
        }
        else
        {
            _autenticacaoView.ExibirMensagem("Nenhum utilizador está autenticado de momento.");
        }
      }
    }


    

