using System;
using System.Collections.Generic;
using MySqlConnector;
using BLOGGGG.data;
using BLOGGGG.Model;

namespace BLOGGGG.Service;

    public class PublicacaoService
    {
        private readonly Conexao _conexao;

        public PublicacaoService(Conexao conexao) 
        {
            _conexao = conexao;
        } 

        public List<Publicacao> ObterTudo()
        {
            var lista = new List<Publicacao>();
            using var conn = _conexao.ObterConexao();
            conn.Open();
            using var cmd = new MySqlCommand("SELECT p.id_publicacao, p.titulo, p.texto, p.data_criacao, u.id_usuario as AutorId, u.Nome as AutorNome, u.Email as AutorEmail FROM publicacao as p INNER JOIN usuario as u ON p.id_usuario = u.id_usuario ORDER BY p.data_criacao DESC", conn);
            using var r = cmd.ExecuteReader();

            while (r.Read())
            {
               lista.Add(new Publicacao
                {
                    
                    Id = r.GetInt32("id_publicacao"),
                    Titulo = r.GetString("titulo"),
                    Conteudo = r.GetString("texto"),
                    DataCriacao = r.GetDateTime("data_criacao"),
                    Autor = new Usuario
                    {
                        Nome = r.GetString("AutorNome"),
                        Email = r.GetString("AutorEmail")
                    }
                });
            }
            return lista;
        }
    }
