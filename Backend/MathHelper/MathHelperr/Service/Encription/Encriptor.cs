
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MathHelperr.Service.Encription;

public class Encriptor : IEncription
{
    private string Key;
    private string Iv;
    private IConfiguration _configuration;

    public Encriptor(IConfiguration configuration)
    {
        _configuration = configuration;
        Key = _configuration["AES_KEY"];
        Console.WriteLine($"key: {Key}");
        Iv = _configuration["AES_IV"];
    }
    
    private static byte[] EncryptWithAES(string plainText, string key, string iv)
    {
        Console.WriteLine($"key: {key}");
        using (Aes aes = Aes.Create())
        {
            aes.Key = Encoding.UTF8.GetBytes(key);
            aes.IV = Encoding.UTF8.GetBytes(iv);
            aes.Mode = CipherMode.CBC;
            aes.Padding = PaddingMode.PKCS7;

            using (MemoryStream ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    byte[] bytes = Encoding.UTF8.GetBytes(plainText);
                    cs.Write(bytes, 0, bytes.Length);
                    cs.FlushFinalBlock();
                }

                return ms.ToArray();
            }
        }
    }

    public string GetEncriptedData(int toEncript)
    {
        byte[] encriptedBytes = EncryptWithAES(toEncript.ToString(), Key, Iv);
        string encryptedString = Convert.ToBase64String(encriptedBytes);
        return encryptedString;
    }
}