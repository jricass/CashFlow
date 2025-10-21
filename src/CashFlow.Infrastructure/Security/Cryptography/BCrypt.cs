using CashFlow.Domain.Security.Cryptography;
using BC = BCrypt.Net.BCrypt;

namespace CashFlow.Infrastructure.Security.Cryptography;

public class BCrypt : IPasswordEncripter
{
    // Recebe uma senha em texto puro e usa 'BC.HashPassword' para gerar um hash seguro.
    public string Encrypt(string password)
    {
        string passwordHash = BC.HashPassword(password);

        // Retorna o Hash gerado.
        return passwordHash;
    }

    // Recebe uma senha em texto puro e um hash. Compara se a senha corresponde ao hash. Retorna true ou false.
    public bool Verify(string passowrd, string passwordHash) => BC.Verify(passowrd, passwordHash);
}

/*
Essa classe serve para criptografar senhas e verificar senhas usando o algoritmo BCrypt.
Usada para garantir segurança ao armazenar e validar senhas de usuários.
*/