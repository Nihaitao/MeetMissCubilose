using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Globalization;

/// <summary>
///jiami 的摘要说明
/// </summary>
public class PlayJiaMi
{
    static string _key = "chutoujy";
    static string _iv = "chutoujy";

    /// <summary>
    /// 地址加密
    /// </summary>
    /// <param name="input"></param>
    /// <param name="key"></param>
    /// <param name="iv"></param>
    /// <returns></returns>
    public static string jiami(string input)
    {
        byte[] key = System.Text.Encoding.ASCII.GetBytes(_key);
        byte[] iv = System.Text.Encoding.ASCII.GetBytes(_iv);
        if ((input == null) || (input.Length == 0))
        {
            return string.Empty;
        }
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        MemoryStream stream = null;
        CryptoStream stream2 = null;
        StreamWriter writer = null;
        string str = string.Empty;
        try
        {
            stream = new MemoryStream();
            stream2 = new CryptoStream(stream, provider.CreateEncryptor(key, iv), CryptoStreamMode.Write);
            writer = new StreamWriter(stream2);
            writer.Write(input);
            writer.Flush();
            stream2.FlushFinalBlock();
            stream.Flush();
            str = Convert.ToBase64String(stream.GetBuffer(), 0, Convert.ToInt32(stream.Length, CultureInfo.InvariantCulture));
        }
        finally
        {
            if (writer != null)
            {
                writer.Close();
            }
            if (stream2 != null)
            {
                stream2.Close();
            }
            if (stream != null)
            {
                stream.Close();
            }
        }
        return str;
    }

    /// <summary>
    /// 地址解密
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    public static string jiemi(string input)
    {
        byte[] key = System.Text.Encoding.ASCII.GetBytes(_key);
        byte[] iv = System.Text.Encoding.ASCII.GetBytes(_iv);
        byte[] buffer;
        try
        {
            buffer = Convert.FromBase64String(input);
        }
        catch (ArgumentNullException)
        {
            return string.Empty;
        }
        catch (FormatException)
        {
            return string.Empty;
        }
        DESCryptoServiceProvider provider = new DESCryptoServiceProvider();
        MemoryStream stream = null;
        CryptoStream stream2 = null;
        StreamReader reader = null;
        string str = string.Empty;
        try
        {
            stream = new MemoryStream(buffer);
            stream2 = new CryptoStream(stream, provider.CreateDecryptor(key, iv), CryptoStreamMode.Read);
            reader = new StreamReader(stream2);
            str = reader.ReadToEnd();
        }
        finally
        {
            if (reader != null)
            {
                reader.Close();
            }
            if (stream2 != null)
            {
                stream2.Close();
            }
            if (stream != null)
            {
                stream.Close();
            }
        }
        return str;
    }


}