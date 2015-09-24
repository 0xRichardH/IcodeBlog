// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EncryptExtension.cs" company="Wedn.Net">
//   Copyright © 2014 Wedn.Net. All Rights Reserved.
// </copyright>
// <summary>
//   加密拓展方法类
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Abp.Encrypt
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// 加密拓展方法类
    /// </summary>
    /// <remarks>
    ///  2013-11-18 18:53 Created By iceStone
    /// </remarks>
    public static class EncryptHelper
    {
        #region MD5加密 +static string Md5(this string str)
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <returns>加密后的字符串</returns>
        public static string Md5(this string str)
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var sbd = new StringBuilder();
            foreach (var item in bytes)
            {
                sbd.Append(item.ToString("x2"));
            }
            return sbd.ToString();
        }
        #endregion

        #region 基于MD5的自定义加密字符串方法（非MD5） +static string Encrypt(this string str, string key = "iceStone")
        /// <summary>
        /// 基于MD5的自定义加密字符串方法(不可逆)（非MD5）
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string Encrypt(this string str, string key = "iceStone")
        {
            var md5 = MD5.Create();

            // 计算字符串的散列值
            var bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(str));
            var eKey = new StringBuilder();
            foreach (var item in bytes)
            {
                eKey.Append(item.ToString("x"));
            }

            // 字符串散列值+密钥再次计算散列值
            bytes = md5.ComputeHash(Encoding.UTF8.GetBytes(key + eKey));
            var pwd = new StringBuilder();
            foreach (var item in bytes)
            {
                pwd.Append(item.ToString("x"));
            }

            return pwd.ToString();
        }
        #endregion

        #region 短加密 +static string ShortEncrypt(this string url, string key = "iceStone")
        /// <summary>
        /// 短加密
        /// </summary>
        /// <param name="url"></param>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string ShortEncrypt(this string url, string key = "iceStone")
        {
            ////要使用生成URL的字符
            // var chars = new[]{
            // "a","b","c","d","e","f","g","h",
            // "i","j","k","l","m","n","o","p",
            // "q","r","s","t","u","v","w","x",
            // "y","z","0","1","2","3","4","5",
            // "6","7","8","9","A","B","C","D",
            // "E","F","G","H","I","J","K","L",
            // "M","N","O","P","Q","R","S","T",
            // "U","V","W","X","Y","Z"

            // };
            ////对传入网址进行MD5加密
            // string hex = System.Web.Security.FormsAuthentication.HashPasswordForStoringInConfigFile(key + url, "md5");

            // string[] resUrl = new string[4];

            // for (int i = 0; i < 4; i++)
            // {
            // //把加密字符按照8位一组16进制与0x3FFFFFFF进行位与运算
            // int hexint = 0x3FFFFFFF & Convert.ToInt32("0x" + hex.Substring(i * 8, 8), 16);
            // string outChars = string.Empty;
            // for (int j = 0; j < 6; j++)
            // {
            // //把得到的值与0x0000003D进行位与运算，取得字符数组chars索引
            // int index = 0x0000003D & hexint;
            // //把取得的字符相加
            // outChars += chars[index];
            // //每次循环按位右移5位
            // hexint = hexint >> 5;
            // }
            // //把字符串存入对应索引的输出数组
            // resUrl[i] = outChars;
            // }

            // return resUrl;
            return string.Empty;
        }
        #endregion

        #region 加密一个字符串 +static string EncryptStr(this string str, string key = "iceStone")
        /// <summary>
        /// 加密一个字符串(可逆，非固定)
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要加密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>加密后的字符串</returns>
        public static string EncryptStr(this string str, string key = "iceStone")
        {
            var des = DES.Create();

            // var timestamp = DateTime.Now.ToString("HHmmssfff");
            var inputBytes = Encoding.UTF8.GetBytes(MixUp(str));
            var keyBytes = Encoding.UTF8.GetBytes(key);
            SHA1 ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyBytes);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    var ret = new StringBuilder();
                    foreach (var b in ms.ToArray())
                    {
                        ret.AppendFormat("{0:X2}", b);
                    }

                    return ret.ToString();
                }
            }
        }
        #endregion

        #region 解密一个字符串 +static string DecryptStr(this string str, string key = "iceStone")
        /// <summary>
        /// 解密一个字符串
        /// </summary>
        /// <remarks>
        ///  2013-11-18 18:53 Created By iceStone
        /// </remarks>
        /// <param name="str">要解密的字符串</param>
        /// <param name="key">加密密钥</param>
        /// <returns>解密后的字符串</returns>
        public static string DecryptStr(this string str, string key = "iceStone")
        {
            var des = DES.Create();
            var inputBytes = new byte[str.Length / 2];
            for (var x = 0; x < str.Length / 2; x++)
            {
                inputBytes[x] = (byte)System.Convert.ToInt32(str.Substring(x * 2, 2), 16);
            }
            var keyByteArray = Encoding.UTF8.GetBytes(key);
            var ha = new SHA1Managed();
            var hb = ha.ComputeHash(keyByteArray);
            var sKey = new byte[8];
            var sIv = new byte[8];
            for (var i = 0; i < 8; i++)
                sKey[i] = hb[i];
            for (var i = 8; i < 16; i++)
                sIv[i - 8] = hb[i];
            des.Key = sKey;
            des.IV = sIv;
            using (var ms = new MemoryStream())
            {
                using (var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write))
                {
                    cs.Write(inputBytes, 0, inputBytes.Length);
                    cs.FlushFinalBlock();
                    return ClearUp(Encoding.UTF8.GetString(ms.ToArray()));
                }
            }
        }
        #endregion

        #region 混淆与反混淆

        // private const string TimestampFormat = "ddHHmmssfff";

        /// <summary>
        /// The timestamp length.
        /// </summary>
        private const int TimestampLength = 36;

        /// <summary>
        /// 用时间简单混淆
        /// </summary>
        /// <param name="str">原字符串</param>
        /// <returns>混淆后字符串</returns>
        public static string MixUp(string str)
        {
            // var timestamp = DateTime.Now.ToString(TimestampFormat);
            var timestamp = Guid.NewGuid().ToString();
            var count = str.Length + TimestampLength;
            var sbd = new StringBuilder(count);
            int j = 0;
            int k = 0;
            for (int i = 0; i < count; i++)
            {
                if (j < TimestampLength && k < str.Length)
                {
                    if (i % 2 == 0)
                    {
                        sbd.Append(str[k]);
                        k++;
                    }
                    else
                    {
                        sbd.Append(timestamp[j]);
                        j++;
                    }
                }
                else if (j >= TimestampLength)
                {
                    sbd.Append(str[k]);
                    k++;
                }
                else if (k >= str.Length)
                {
                    break;

                    // sbd.Append(timestamp[j]);
                    // j++;
                }
            }

            return sbd.ToString();
        }

        /// <summary>
        /// 简单反混淆
        /// </summary>
        /// <param name="str">混淆后字符串</param>
        /// <returns>原来字符串</returns>
        public static string ClearUp(string str)
        {
            var sbd = new StringBuilder();
            int j = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (i % 2 == 0)
                {
                    sbd.Append(str[i]);
                }
                else
                {
                    j++;
                }

                if (j > TimestampLength)
                {
                    sbd.Append(str.Substring(i));
                    break;
                }
            }

            return sbd.ToString();
        }

        #endregion

        #region 哈希加密一个字符串 + public static string HashEncoding(this string security)
        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
        /// <param name="security"></param>
        /// <returns></returns>
        public static string HashEncoding(this string security)
        {
            UnicodeEncoding code = new UnicodeEncoding();
            byte[] Message = code.GetBytes(security);
            SHA512Managed arithmetic = new SHA512Managed();
            var value = arithmetic.ComputeHash(Message);
            StringBuilder builder = new StringBuilder();
            foreach (byte o in value)
            {
                builder.Append((int)o + "O");
            }
            return builder.ToString();
        } 
        #endregion

        #region SHA1加密 + public static string SHA1EncryptBy(this string input)
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="input"></param>
        /// <des>哈希值用作表示大量数据的固定大小的唯一值。 如果相应的数据也匹配，则两个数据集的哈希应该匹配。 数据的少量更改会在哈希值中产生不可预知的大量更改。SHA1 算法的哈希值大小为 160 位。</des>
        /// <returns></returns>
        public static string SHA1EncryptBy(this string input)
        {
            SHA1 sha = new SHA1CryptoServiceProvider();
            byte[] bytes = Encoding.Unicode.GetBytes(input);
            byte[] result = sha.ComputeHash(bytes);
            return BitConverter.ToString(result);
        } 
        #endregion

        #region RSA加密 +  public static string RSAEncrypt(this string plaintext, string publicKey)
        /// <summary>
        /// RSA加密
        /// </summary>
        /// <param name="plaintext">明文</param>
        /// <param name="publicKey">公钥</param>
        /// <returns>密文字符串</returns>
        public static string RSAEncrypt(this string plaintext, string publicKey)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            byte[] dataToEncrypt = byteConverter.GetBytes(plaintext);
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] encryptedData = rsa.Encrypt(dataToEncrypt, false);
                return Convert.ToBase64String(encryptedData);
            }
        } 
        #endregion

        #region RSA解密 + public static string RSADecrypt(this string ciphertext, string privateKey)
        /// <summary>
        /// RSA解密
        /// </summary>
        /// <param name="ciphertext">密文</param>
        /// <param name="privateKey">私钥</param>
        /// <returns>明文字符串</returns>
        public static string RSADecrypt(this string ciphertext, string privateKey)
        {
            UnicodeEncoding byteConverter = new UnicodeEncoding();
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                byte[] encryptedData = Convert.FromBase64String(ciphertext);
                byte[] decryptedData = rsa.Decrypt(encryptedData, false);
                return byteConverter.GetString(decryptedData);
            }
        } 
        #endregion

        #region 字符串Base64编码 + public static string ToBase64(this string input)
        /// <summary>
        /// 字符串Base64编码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string ToBase64(this string input)
        {
            var bytes = Encoding.Default.GetBytes(input);
            return Convert.ToBase64String(bytes);
        } 
        #endregion

        #region 字符串Base64解码 + public static string FromBase64(this string input)
        /// <summary>
        /// 字符串Base64解码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string FromBase64(this string input)
        {
            var output = Convert.FromBase64String(input);
            return Encoding.Default.GetString(output);
        } 
        #endregion
    }
}