using System.Security.Cryptography;
using System.Text;

namespace WebApi.Shared.Services.PasswordHashService;

public sealed class PasswordHashService : IEncryptService
{
    public string Encrypt(string password)
    {
        // Generar un salt aleatorio
        byte[] salt = new byte[16];
        using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
        {
            rng.GetBytes(salt);
        }

        // Generar el hash de la contraseña con el salt y 10000 iteraciones utilizando AES
        byte[] hash = PBKDF2(password, salt, 10000, 32);

        // Combinar el salt y el hash
        byte[] hashBytes = new byte[48];
        Array.Copy(salt, 0, hashBytes, 0, 16);
        Array.Copy(hash, 0, hashBytes, 16, 32);

        // Convertir a formato base64 para almacenamiento
        string hashedPassword = Convert.ToBase64String(hashBytes);

        return hashedPassword;
    }

    public bool VerifyEncrypt(string password, string hashedPassword)
    {
        // Extraer el salt y el hash de la contraseña almacenada
        byte[] hashBytes = Convert.FromBase64String(hashedPassword);
        byte[] salt = new byte[16];
        Array.Copy(hashBytes, 0, salt, 0, 16);

        // Generar el hash de la contraseña de entrada con el salt almacenado y 10000 iteraciones utilizando AES
        byte[] hash = PBKDF2(password, salt, 10000, 32);

        // Comparar los hashes
        for (int i = 0; i < 32; i++)
        {
            if (hashBytes[i + 16] != hash[i])
            {
                return false;
            }
        }
        return true;
    }

    #region Private Methods

    private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
    {
        using var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(password));
        byte[] counter = BitConverter.GetBytes(1); // Inicializamos el contador en 1
        byte[] result = new byte[outputBytes];
        byte[] buffer = new byte[salt.Length + counter.Length];

        // Iteramos el algoritmo PBKDF2
        for (int i = 0; i < iterations; i++)
        {
            // Combinamos el salt y el contador
            Array.Copy(salt, buffer, salt.Length);
            Array.Copy(counter, 0, buffer, salt.Length, counter.Length);

            // Generamos el bloque de derivación
            byte[] block = hmac.ComputeHash(buffer);

            // XOR con el resultado actual
            for (int j = 0; j < outputBytes; j++)
            {
                result[j] ^= block[j];
            }

            // Incrementamos el contador
            IncrementCounter(counter);
        }

        return result;
    }

    private static void IncrementCounter(byte[] counter)
    {
        for (int i = counter.Length - 1; i >= 0; i--)
        {
            if (++counter[i] != 0)
                break;
        }
    }

    #endregion Private Methods
}