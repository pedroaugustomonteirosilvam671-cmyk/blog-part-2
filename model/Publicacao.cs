using System;
using System.Collections.Generic;

namespace BLOGGGG.Model
{
    

    public class Publicacao
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = string.Empty;
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public Usuario Autor { get; set; } = new Usuario();
    }
}