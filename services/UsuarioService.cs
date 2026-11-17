using System;
using System.Threading.Tasks;
using MySqlConnector;
using BLOGGGG.Model;
using BLOGGGG.Service;

namespace BLOGGGG.Service;

public class UsuarioService
{
    private readonly string _connectionString;
    private readonly SenhaService _senhaService;

    public UsuarioService(string connectionString, SenhaService senhaService)
    {
        _connectionString = connectionString;
        _senhaService = senhaService;
    }

    public async Task<(bool Sucesso, string Mensagem, Usuario? Usuario)> CadastrarAsync(string nome, string email, string senha)
    {
        nome = nome?.Trim() ?? string.Empty;
        email = email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(nome) || string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            return (false, "Nome, e-mail e senha são obrigatórios.", null);

        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        string sqlCheck = "SELECT COUNT(*) FROM usuario WHERE email = @email";
        using var cmdCheck = new MySqlCommand(sqlCheck, connection);
        cmdCheck.Parameters.AddWithValue("@email", email);
        
        long count = Convert.ToInt64(await cmdCheck.ExecuteScalarAsync() ?? 0);
        if (count > 0)
            return (false, "Erro: Este e-mail já se encontra registado.", null);

        string hashSenha = _senhaService.GerarHash(senha);

        string sqlInsert = @"
            INSERT INTO usuario (nome, email, senha) 
            VALUES (@nome, @email, @senha);
            SELECT LAST_INSERT_ID();";

        using var cmdInsert = new MySqlCommand(sqlInsert, connection);
        cmdInsert.Parameters.AddWithValue("@nome", nome);
        cmdInsert.Parameters.AddWithValue("@email", email);
        cmdInsert.Parameters.AddWithValue("@senha", hashSenha);

        ulong idGerado = Convert.ToUInt64(await cmdInsert.ExecuteScalarAsync());

        var novoUsuario = new Usuario
        {
            Id = (int)idGerado,
            Nome = nome,
            Email = email
        };

        return (true, "Conta criada com sucesso!", novoUsuario);
    }

    public async Task<(bool Sucesso, string Mensagem, Usuario? Usuario)> AutenticarAsync(string email, string senha)
    {
        email = email?.Trim().ToLowerInvariant() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(senha))
            return (false, "E-mail e senha são obrigatórios.", null);

        using var connection = new MySqlConnection(_connectionString);
        await connection.OpenAsync();

        string sql = "SELECT id_usuario, nome, email, senha FROM usuario WHERE email = @email";
        using var cmd = new MySqlCommand(sql, connection);
        cmd.Parameters.AddWithValue("@email", email);

        using var reader = await cmd.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
            return (false, "E-mail ou senha incorretos.", null);

        int id = reader.GetInt32("id_usuario");
        string nome = reader.GetString("nome");
        string hash = reader.GetString("senha");

        if (!_senhaService.Verificar(senha, hash))
            return (false, "E-mail ou senha incorretos.", null);

        var usuario = new Usuario
        {
            Id = id,
            Nome = nome,
            Email = email
        };

        return (true, $"Bem-vindo(a), {nome}!", usuario);
    }
}