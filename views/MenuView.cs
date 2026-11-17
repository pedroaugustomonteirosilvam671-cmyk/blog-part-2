namespace BLOGGGG.View
{
    using System;

    public class MenuView
    {
        public void ExibirMenu()
        {
            Console.WriteLine("=== MENU PRINCIPAL ===");
            Console.WriteLine("1 - Ver publicaçoes");
            Console.WriteLine("2 - Criar publicaçao");
            Console.WriteLine("3 - adicionar comentario");
            Console.WriteLine("4 - atualizar publicaçao");
            Console.WriteLine("0 - sair");
        }

        public string ObterEntradaUsuario()
        {
            Console.Write("Escolha uma opçao ");
            return Console.ReadLine() ?? string.Empty;
        }
    }
}