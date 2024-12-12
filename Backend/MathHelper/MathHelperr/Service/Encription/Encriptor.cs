
using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace MathHelperr.Service.Encription;

public class Encriptor : IEncription
{
    private string Key;
    private string Iv;

    public Encriptor()
    {
        Key = Environment.GetEnvironmentVariable("AES_KEY");
        Iv = Environment.GetEnvironmentVariable("AES_IV");
    }
    
    private static byte[] EncryptWithAES(string plainText, string key, string iv)
    {
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
        string base64 = Convert.ToBase64String(encriptedBytes);
        return base64;
    }
}