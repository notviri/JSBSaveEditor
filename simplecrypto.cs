using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

public class SimpleCrypto {
    public static string Encrypt(string clearText, string Password) {
        byte[] bytes = Encoding.Unicode.GetBytes(clearText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
        return Convert.ToBase64String(Encrypt(bytes, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
    }

    public static string EncryptDecryptXOR(string textToEncrypt) {
        int num = 129;
        StringBuilder stringBuilder1 = new StringBuilder(textToEncrypt);
        StringBuilder stringBuilder2 = new StringBuilder(textToEncrypt.Length);
        for (int index = 0; index < textToEncrypt.Length; ++index) {
            char ch = (char)(stringBuilder1[index] ^ (uint)num);
            stringBuilder2.Append(ch);
        }
        return stringBuilder2.ToString();
    }

    public static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV) {
        MemoryStream memoryStream = new MemoryStream();
        Rijndael rijndael = Rijndael.Create();
        rijndael.Key = Key;
        rijndael.IV = IV;
        CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(clearData, 0, clearData.Length);
        cryptoStream.Close();
        return memoryStream.ToArray();
    }

    public static byte[] Encrypt(byte[] clearData, string Password) {
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
        return Encrypt(clearData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
    }

    public static void Encrypt(string fileIn, string fileOut, string Password) {
        FileStream fileStream1 = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
        FileStream fileStream2 = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13]
        { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118
        });
        Rijndael rijndael = Rijndael.Create();
        rijndael.Key = passwordDeriveBytes.GetBytes(32);
        rijndael.IV = passwordDeriveBytes.GetBytes(16);
        CryptoStream cryptoStream = new CryptoStream(fileStream2, rijndael.CreateEncryptor(), CryptoStreamMode.Write);
        int count1 = 4096;
        byte[] buffer = new byte[count1];
        int count2;
        do {
            count2 = fileStream1.Read(buffer, 0, count1);
            cryptoStream.Write(buffer, 0, count2);
        }
        while (count2 != 0);
        cryptoStream.Close();
        fileStream1.Close();
    }

    public static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV) {
        MemoryStream memoryStream = new MemoryStream();
        Rijndael rijndael = Rijndael.Create();
        rijndael.Key = Key;
        rijndael.IV = IV;
        CryptoStream cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
        cryptoStream.Write(cipherData, 0, cipherData.Length);
        cryptoStream.Close();
        return memoryStream.ToArray();
    }

    public static string Decrypt(string cipherText, string Password) {
        byte[] cipherData = Convert.FromBase64String(cipherText);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
        return Encoding.Unicode.GetString(Decrypt(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16)));
    }

    public static byte[] Decrypt(byte[] cipherData, string Password) {
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13] { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118 });
        return Decrypt(cipherData, passwordDeriveBytes.GetBytes(32), passwordDeriveBytes.GetBytes(16));
    }

    public static string DecryptToString(byte[] cipherData, string Password) {
        return Encoding.Unicode.GetString(Decrypt(cipherData, Password));
    }

    public static void Decrypt(string fileIn, string fileOut, string Password) {
        FileStream fileStream1 = new FileStream(fileIn, FileMode.Open, FileAccess.Read);
        FileStream fileStream2 = new FileStream(fileOut, FileMode.OpenOrCreate, FileAccess.Write);
        PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, new byte[13]
        { 73, 118, 97, 110, 32, 77, 101, 100, 118, 101, 100, 101, 118
        });
        Rijndael rijndael = Rijndael.Create();
        rijndael.Key = passwordDeriveBytes.GetBytes(32);
        rijndael.IV = passwordDeriveBytes.GetBytes(16);
        CryptoStream cryptoStream = new CryptoStream(fileStream2, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
        int count1 = 4096;
        byte[] buffer = new byte[count1];
        int count2;
        do {
            count2 = fileStream1.Read(buffer, 0, count1);
            cryptoStream.Write(buffer, 0, count2);
        }
        while (count2 != 0);
        cryptoStream.Close();
        fileStream1.Close();
    }

    public static byte[] loadByte(string fileIn) {
        if (File.Exists(fileIn))
            return File.ReadAllBytes(fileIn);
        // UnityEngine Call - Debug.LogError((object)"Cannot Read From A Null File");
        return (byte[])null;
    }
}