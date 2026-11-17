using BLOGGGG.Service;
using BLOGGGG.View;


namespace BLOGGGG.Controller;

    public class PublicacaoController
    {
        private readonly PublicacaoService _service;
        private readonly PublicacaoView _view;

        public PublicacaoController(PublicacaoService service, PublicacaoView view)
        {
            _service = service;
            _view = view;
        }

        public void CarregarPublicacoes()
        {
            var publicacoes = _service.ObterTudo();
            foreach (var p in publicacoes)
            {
                _view.ExibirPublicacao(p);
            }
        }

        public void CriarPublicacao()
        {
            // Lógica para criar uma publicação
        }

        public void AdicionarComentario()
        {
            var novoComentario = _view.SolicitarComentario();
            
        }

        public void NavegarMenu()
        {
           
        }
    }
