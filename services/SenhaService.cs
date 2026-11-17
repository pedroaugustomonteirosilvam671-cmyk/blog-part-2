using System;
using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace BLOGGGG.Service
{
    public class SenhaService
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int DegreeOfParallelism = 8;
        private const int MemorySize = 65536;
        private const int Iterations = 4;

        public string GerarHash(string senha)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);

            using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senha))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySize,
                Iterations = Iterations
            };

            byte[] hash = argon2.GetBytes(HashSize);

            byte[] hashComSalt = new byte[SaltSize + HashSize];
            Buffer.BlockCopy(salt, 0, hashComSalt, 0, SaltSize);
            Buffer.BlockCopy(hash, 0, hashComSalt, SaltSize, HashSize);

            return Convert.ToBase64String(hashComSalt);
        }

        public bool Verificar(string senhaInformada, string hashGuardadoBase64)
        {
            try
            {
                byte[] hashComSalt = Convert.FromBase64String(hashGuardadoBase64);

                if (hashComSalt.Length != SaltSize + HashSize)
                    return false;

                byte[] salt = new byte[SaltSize];
                Buffer.BlockCopy(hashComSalt, 0, salt, 0, SaltSize);

                byte[] hashOriginal = new byte[HashSize];
                Buffer.BlockCopy(hashComSalt, SaltSize, hashOriginal, 0, HashSize);

                using var argon2 = new Argon2id(Encoding.UTF8.GetBytes(senhaInformada))
                {
                    Salt = salt,
                    DegreeOfParallelism = DegreeOfParallelism,
                    MemorySize = MemorySize,
                    Iterations = Iterations
                };

                byte[] novoHash = argon2.GetBytes(HashSize);

                return CryptographicOperations.FixedTimeEquals(hashOriginal, novoHash);
            }
            catch
            {
                return false;
            }
        }
    }
}