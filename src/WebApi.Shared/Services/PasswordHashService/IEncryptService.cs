namespace WebApi.Shared.Services.PasswordHashService;

public interface IEncryptService
{
    /// <summary>
    /// Encrypts the specified password.
    /// </summary>
    /// <param name="password">Unencrypted password</param>
    /// <returns></returns>
    string Encrypt(string password);
    /// <summary>
    /// Verifies the encrypt.
    /// </summary>
    /// <param name="password">The unencrypted password.</param>
    /// <param name="hashedPassword">The hashed password.</param>
    /// <returns></returns>
    bool VerifyEncrypt(string password, string hashedPassword);
}