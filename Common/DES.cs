using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

/// <summary>
/// 本类完成对字符串的加密和解密。
/// 通过DES对称加密算法,完成对字符串的加密和解密操作。
/// </summary>
public class DES
{
    private SymmetricAlgorithm mCSP;
    private const string CIV = "EDwDes8XcGs=";//密钥
    private const string CKEY = "WSGQWGZESwI=";//初始化向量   

    public DES()
    {
        mCSP = new DESCryptoServiceProvider();
    }

    /// <summary>
    /// 加密
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public string EncryptString(string Value)
    {
        ICryptoTransform ct;
        MemoryStream ms;
        CryptoStream cs;
        byte[] byt;

        ct = mCSP.CreateEncryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV));

        byt = Encoding.UTF8.GetBytes(Value);

        ms = new MemoryStream();
        cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
        cs.Write(byt, 0, byt.Length);
        cs.FlushFinalBlock();

        cs.Close();

        return Convert.ToBase64String(ms.ToArray()).Replace("+", "%2B");
    }

    /// <summary>
    /// 解密
    /// </summary>
    /// <param name="Value"></param>
    /// <returns></returns>
    public string DecryptString(string Value)
    {
        if (Value != null)
        {

            Value = Value.Replace("%2B", "+");
        }
        ICryptoTransform ct;
        MemoryStream ms;
        CryptoStream cs;
        byte[] byt;

        try
        {
            ct = mCSP.CreateDecryptor(Convert.FromBase64String(CKEY), Convert.FromBase64String(CIV));
            byt = Convert.FromBase64String(Value);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ct, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            return Encoding.UTF8.GetString(ms.ToArray());
        }
        catch (Exception ex)
        {
            return null;
        }
    }

}



