using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

public class Encode : MonoBehaviour
{
    private const string AES_IV_256 = @"TD/NhYTFZh5L6u9g";
    private const string AES_Key_256 = @"pVGs8WdrrTV8mAGmgwSMcLm_sXJR3Jmb";

    public static string Encrypt(string data)  //à√çÜâªèàóù
    {
        RijndaelManaged myRijndael = new RijndaelManaged();
        myRijndael.BlockSize = 128;
        myRijndael.KeySize = 256;
        myRijndael.Mode = CipherMode.CBC;
        myRijndael.Padding = PaddingMode.PKCS7;
        myRijndael.IV = Encoding.UTF8.GetBytes(AES_IV_256);
        myRijndael.Key = Encoding.UTF8.GetBytes(AES_Key_256);
        ICryptoTransform encryptor = myRijndael.CreateEncryptor(myRijndael.Key, myRijndael.IV);
        byte[] encrypted;
        using (MemoryStream mStream = new MemoryStream())
        {
            using (CryptoStream ctStream = new CryptoStream(mStream, encryptor, CryptoStreamMode.Write)) //?
            {
                using (StreamWriter sw = new StreamWriter(ctStream))
                {
                    sw.Write(data);
                }
                encrypted = mStream.ToArray();
            }
        }
        return (System.Convert.ToBase64String(encrypted));
    }

    public static string Decrypt(string cipher) //ï°çáèàóù
    {
        RijndaelManaged rijndael = new RijndaelManaged();
        rijndael.BlockSize = 128;
        rijndael.KeySize = 256;
        rijndael.Mode = CipherMode.CBC;
        rijndael.Padding = PaddingMode.PKCS7;
        rijndael.IV = Encoding.UTF8.GetBytes(AES_IV_256);
        rijndael.Key = Encoding.UTF8.GetBytes(AES_Key_256);

        ICryptoTransform decryptor = rijndael.CreateDecryptor(rijndael.Key, rijndael.IV);

        string plain = string.Empty;
        using (MemoryStream mStream = new MemoryStream(System.Convert.FromBase64String(cipher)))
        {
            using (CryptoStream ctStream = new CryptoStream(mStream, decryptor, CryptoStreamMode.Read))
            {
                using (StreamReader sr = new StreamReader(ctStream))
                {
                    plain = sr.ReadToEnd();
                }
            }
        }
        return plain;
    }
}
