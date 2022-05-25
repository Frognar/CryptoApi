using System.Security.Cryptography;
using System.Text;

namespace Crypto;

public class CryptoRSA : ICrypto
{
    private const string PemFileName = "rsa.pem";
    private readonly RSACryptoServiceProvider _rsa;


    public CryptoRSA()
    {
        string pem;
        if (File.Exists(PemFileName))
        {
            pem = File.ReadAllText(PemFileName);
        }
        else
        {
            pem = CreatePEM();
            File.WriteAllText(PemFileName, pem);
        }

        _rsa = new RSACryptoServiceProvider();
        _rsa.ImportFromPem(pem.ToCharArray());
    }

    private string CreatePEM()
    {
        RSA rsa = RSA.Create(4096);
        string hdr = "-----BEGIN RSA PRIVATE KEY-----";
        string priv = Convert.ToBase64String(rsa.ExportRSAPrivateKey());
        string ftr = "-----END RSA PRIVATE KEY-----";
        return $"{hdr}\n{priv}\n{ftr}";
    }

    public Task<string> Encrypt(string textToEncrypt)
    {
        return Task.FromResult(EncryptInternal(textToEncrypt));
    }

    private string EncryptInternal(string textToEncrypt)
    {
        if (string.IsNullOrEmpty(textToEncrypt))
        {
            return string.Empty;
        }

        byte[] messageBytes = Encoding.UTF8.GetBytes(textToEncrypt);
        byte[] encryptedMessageBytes = _rsa.Encrypt(messageBytes, false);
        return Convert.ToBase64String(encryptedMessageBytes);
    }

    public Task<string> Decrypt(string encryptedText)
    {
        return Task.FromResult(DecryptInternal(encryptedText));
    }

    private string DecryptInternal(string encryptedText)
    {

        if (string.IsNullOrEmpty(encryptedText))
        {
            return string.Empty;
        }

        byte[] encryptedMessageBytes = Convert.FromBase64String(encryptedText);
        byte[] messageBytes = _rsa.Decrypt(encryptedMessageBytes, false);
        return Encoding.UTF8.GetString(messageBytes);
    }
}
