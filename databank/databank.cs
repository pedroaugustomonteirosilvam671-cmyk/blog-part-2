namespace BLOGGGG.databank
{
    public static class mysql
    {
        public const string ScriptCriacaoTabelas = @"
CREATE DATABASE IF NOT EXISTS BlogDb;
USE BlogDb;

CREATE TABLE Usuario (
    Id CHAR(36) NOT NULL PRIMARY KEY,
    Nome VARCHAR(100) NOT NULL,
    Email VARCHAR(100) NOT NULL UNIQUE,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP
);

CREATE TABLE Publicacao (
    Id CHAR(36) NOT NULL PRIMARY KEY,
    Titulo VARCHAR(200) NOT NULL,
    Conteudo TEXT NOT NULL,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    UsuarioId CHAR(36) NOT NULL,
    CONSTRAINT FK_Publicacao_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id) ON DELETE CASCADE
);

CREATE TABLE Comentario (
    Id CHAR(36) NOT NULL PRIMARY KEY,
    Conteudo TEXT NOT NULL,
    DataCriacao DATETIME DEFAULT CURRENT_TIMESTAMP,
    UsuarioId CHAR(36) NOT NULL,
    PublicacaoId CHAR(36) NOT NULL,
    CONSTRAINT FK_Comentario_Usuario FOREIGN KEY (UsuarioId) REFERENCES Usuario(Id),
    CONSTRAINT FK_Comentario_Publicacao FOREIGN KEY (PublicacaoId) REFERENCES Publicacao(Id) ON DELETE CASCADE
);";
    }
}