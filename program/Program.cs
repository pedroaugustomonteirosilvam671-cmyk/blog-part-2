using System;
using BLOGGGG.data;
using BLOGGGG.Service;
using BLOGGGG.View;
using BLOGGGG.Controller;

namespace BLOGGGG
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var conexao = new Conexao();
            var service = new PublicacaoService(conexao);
            var viewPublicacao = new PublicacaoView();
            var viewMenu = new MenuView();
            var controller = new PublicacaoController(service, viewPublicacao);

            bool executando = true;

            while (executando)
            {
                Console.Clear();
                viewMenu.ExibirMenu();
                string opcao = viewMenu.ObterEntradaUsuario();

                switch (opcao)
                {
                    case "1":
                        controller.CarregarPublicacoes();
                        break;
                    case "2":
                        controller.CriarPublicacao();
                        break;
                    case "3":
                        controller.AdicionarComentario();
                        break;
                    case "0":
                        executando = false;
                        Console.WriteLine("A encerrar a aplicação...");
                        break;
                    default:
                        Console.WriteLine("Opção inválida!");
                        break;
                }

                if (executando)
                {
                    Console.WriteLine("\nPressione qualquer tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }
    }
}