using System;
using MySqlConnector;

namespace BLOGGGG.data;

    public class Conexao
    {
        private readonly string _connectionString;

        public Conexao()
        {
            _connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING") 
                                 ?? "Server=localhost;Database=BLOG caso de uso;User ID=root;Password=root;Port=3308;";
        }

        public MySqlConnection ObterConexao()
        {
            return new MySqlConnection(_connectionString);
        }
    }
