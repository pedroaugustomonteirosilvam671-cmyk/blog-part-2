using System;
using BLOGGGG.Model;

namespace BLOGGGG.View
{
    public class AutenticacaoView
    {
        public Usuario SolicitarDadosCadastro()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("       CADASTRO DE USUÁRIO       ");
            Console.WriteLine("=================================");

            Console.Write("Digite o seu nome: ");
            string nome = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite o seu e-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Digite a sua senha: ");
            string senha = LerSenhaOculta();

            return new Usuario
            {
                Nome = nome,
                Email = email,
                Senha = senha
            };
        }

        public (string Email, string Senha) SolicitarCredenciais()
        {
            Console.WriteLine("\n=================================");
            Console.WriteLine("        LOGIN DE USUÁRIO         ");
            Console.WriteLine("=================================");

            Console.Write("E-mail: ");
            string email = Console.ReadLine() ?? string.Empty;

            Console.Write("Senha: ");
            string senha = LerSenhaOculta();

            return (email, senha);
        }

        public void ExibirMensagem(string mensagem)
        {
            Console.WriteLine($"\n[SISTEMA]: {mensagem}");
        }

        private string LerSenhaOculta()
        {
            string senha = string.Empty;

            while (true)
            {
                ConsoleKeyInfo tecla = Console.ReadKey(true);

                if (tecla.Key == ConsoleKey.Enter)
                {
                    Console.WriteLine();
                    break;
                }

                if (tecla.Key == ConsoleKey.Backspace)
                {
                    if (senha.Length > 0)
                    {
                        senha = senha.Substring(0, senha.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                else if (!char.IsControl(tecla.KeyChar))
                {
                    senha += tecla.KeyChar;
                    Console.Write("*");
                }
            }

            return senha;
        }
    }
}