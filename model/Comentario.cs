namespace BLOGGGG.Model
{
    using System;

    public class Comentario
    {
        public int Id { get; set; }
        public string Conteudo { get; set; } = string.Empty;
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        public Usuario Autor { get; set; } = new Usuario();
    }
}