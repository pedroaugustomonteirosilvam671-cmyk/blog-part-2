using System;
using System.Collections.Generic;
using BLOGGGG.Model;

namespace BLOGGGG.View;


    public class PublicacaoView
    {
        public void ExibirPublicacao(Publicacao p)
        {
            Console.WriteLine($"\n[{p.Id}]  {p.Titulo}\nAutor: {p.Autor.Nome}\n{p.Conteudo}");

        }

        public Comentario SolicitarComentario()
        {
            Console.Write("digite o comentario");
            return null;
        }
    }
