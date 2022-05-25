namespace Crypto;

public interface ICrypto
{
    Task<string> Decrypt(string encryptedText);
    Task<string> Encrypt(string textToEncrypt);
}
