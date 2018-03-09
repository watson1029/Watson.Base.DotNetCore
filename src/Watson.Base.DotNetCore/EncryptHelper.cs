using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Watson.Base.DotNetCore.EncryptHelper
{

    #region DES加密算法
    /// <summary>
    /// DES加密算法基础类
    /// </summary>
    public class DES
    {
        private string Key;
        private string IV;
        private DESCryptoServiceProvider des;

        #region DES算法构造函数
        /// <summary>
        /// DES算法构造函数（KEY和IV随机生成）
        /// </summary>
        public DES()
        {
            des = new DESCryptoServiceProvider();
            des.GenerateKey();
            des.GenerateIV();
            Key = Convert.ToBase64String(des.Key);
            IV = Convert.ToBase64String(des.IV);
        }

        /// <summary>
        /// DES算法构造函数（KEY自定义，IV随机生成）
        /// </summary>
        /// <param name="sKey"></param>
        public DES(string sKey)
        {
            des = new DESCryptoServiceProvider();
            des.GenerateIV();
            Key = sKey;
            IV = Convert.ToBase64String(des.IV);
        }

        /// <summary>
        /// DES算法构造函数（KEY和IV自定义）
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        public DES(string sKey, string sIV)
        {
            des = new DESCryptoServiceProvider();
            Key = sKey;
            IV = sIV;
        }
        #endregion

        #region Get方法
        /// <summary>
        /// 获取Key
        /// </summary>
        public string getKey
        {
            get
            {
                return Key;
            }
        }

        /// <summary>
        /// 获取IV
        /// </summary>
        public string getIV
        {
            get
            {
                return IV;
            }
        }
        #endregion

        #region 获取Key和IV的私有方法
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            des.GenerateKey();
            byte[] bytTemp = des.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = IV;
            des.GenerateIV();
            byte[] bytTemp = des.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        #endregion

        #region 加密与解密
        ///<summary>
        /// DES加密方法（失败返回NULL）
        ///</summary>
        ///<param name="strText">明文</param>
        ///<returns>密文</returns>
        public string Encrypt(string strText)
        {
            try
            {
                byte[] bytIn = UTF8Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                des.Key = GetLegalKey();
                des.IV = GetLegalIV();
                ICryptoTransform encrypto = des.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                ms.Close();
                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
            catch
            {
                return null;
            }
        }

        ///<summary>
        /// DES解密方法（失败返回NULL）
        ///</summary>
        ///<param name="strText">密文</param>
        ///<returns>明文</returns>
        public string Decrypt(string strText)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                des.Key = GetLegalKey();
                des.IV = GetLegalIV();
                ICryptoTransform encrypto = des.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
    #endregion

    #region TripleDES加密算法
    /// <summary>
    /// TripleDES加密算法抽象基础类
    /// </summary>
    public class TripleDES
    {
        private string Key;
        private string IV;
        private TripleDESCryptoServiceProvider des;

        #region TripleDES算法构造函数
        /// <summary>
        /// TripleDES算法构造函数（KEY和IV随机生成）
        /// </summary>
        public TripleDES()
        {
            des = new TripleDESCryptoServiceProvider();
            des.GenerateKey();
            des.GenerateIV();
            Key = Convert.ToBase64String(des.Key);
            IV = Convert.ToBase64String(des.IV);
        }

        /// <summary>
        /// TripleDES算法构造函数（KEY自定义，IV随机生成）
        /// </summary>
        /// <param name="sKey"></param>
        public TripleDES(string sKey)
        {
            des = new TripleDESCryptoServiceProvider();
            des.GenerateIV();
            Key = sKey;
            IV = Convert.ToBase64String(des.IV);
        }

        /// <summary>
        /// TripleDES算法构造函数（KEY和IV自定义）
        /// </summary>
        /// <param name="sKey"></param>
        /// <param name="sIV"></param>
        public TripleDES(string sKey, string sIV)
        {
            des = new TripleDESCryptoServiceProvider();
            Key = sKey;
            IV = sIV;
        }
        #endregion

        #region Get方法
        /// <summary>
        /// 获取Key
        /// </summary>
        public string getKey
        {
            get
            {
                return Key;
            }
        }

        /// <summary>
        /// 获取IV
        /// </summary>
        public string getIV
        {
            get
            {
                return IV;
            }
        }
        #endregion

        #region 获取Key和IV的私有方法
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            des.GenerateKey();
            byte[] bytTemp = des.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = IV;
            des.GenerateIV();
            byte[] bytTemp = des.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        #endregion

        #region 加密与解密
        ///<summary>
        /// TripleDES加密方法（失败返回NULL）
        ///</summary>
        ///<param name="strText">明文</param>
        ///<returns>密文</returns>
        public string Encrypt(string strText)
        {
            try
            {
                byte[] bytIn = UTF8Encoding.UTF8.GetBytes(strText);
                MemoryStream ms = new MemoryStream();
                des.Key = GetLegalKey();
                des.IV = GetLegalIV();
                ICryptoTransform encrypto = des.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                ms.Close();
                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
            catch
            {
                return null;
            }
        }

        ///<summary>
        /// TripleDES解密方法（失败返回NULL）
        ///</summary>
        ///<param name="strText">密文</param>
        ///<returns>明文</returns>
        public string Decrypt(string strText)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(strText);
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                des.Key = GetLegalKey();
                des.IV = GetLegalIV();
                ICryptoTransform encrypto = des.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }

        #endregion
    }
    #endregion

    #region AES加密算法
    /// <summary>
    /// AES加密算法基础类
    /// </summary>
    public class AES
    {
        private RijndaelManaged aes;
        private string Key;
        private string IV;

        #region AES算法构造函数
        /// <summary>
        /// AES算法构造函数（KEY和IV随机生成）
        /// </summary>
        public AES()
        {
            aes = new RijndaelManaged();
            aes.GenerateKey();
            aes.GenerateIV();
            Key = Convert.ToBase64String(aes.Key);
            IV = Convert.ToBase64String(aes.IV);
        }

        /// <summary>
        /// AES构造函数（KEY自定义，IV随机生成）
        /// </summary>
        /// <param name="sKey">32位KEY</param>
        public AES(string sKey)
        {
            aes = new RijndaelManaged();
            aes.GenerateIV();
            Key = sKey;
            IV = Convert.ToBase64String(aes.IV);
        }

        /// <summary>
        /// AES构造函数（KEY和IV自定义）
        /// </summary>
        /// <param name="sKey">32位KEY</param>
        /// <param name="sIV">16位IV</param>
        public AES(string sKey, string sIV)
        {
            aes = new RijndaelManaged();
            Key = sKey;
            IV = sIV;
        }
        #endregion

        #region Get方法
        /// <summary>
        /// 获取KEY
        /// </summary>
        public string getKey
        {
            get
            {
                return Key;
            }
        }
        /// <summary>
        /// 获取IV
        /// </summary>
        public string getIV
        {
            get
            {
                return IV;
            }
        }
        #endregion

        #region 获取KEY和IV的私有方法
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            aes.GenerateKey();
            byte[] bytTemp = aes.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = IV;
            aes.GenerateIV();
            byte[] bytTemp = aes.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        #endregion

        #region String加密解密操作
        /// <summary>
        /// AES加密方法（失败返回NULL）
        /// </summary>
        /// <param name="Source">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string Source)
        {
            try
            {
                byte[] bytIn = UTF8Encoding.UTF8.GetBytes(Source);
                MemoryStream ms = new MemoryStream();
                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();
                ICryptoTransform encrypto = aes.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                ms.Close();
                byte[] bytOut = ms.ToArray();
                return Convert.ToBase64String(bytOut);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// AES解密方法（失败返回NULL）
        /// </summary>
        /// <param name="Source">密文</param>
        /// <returns>明文</returns>
        public string Decrypt(string Source)
        {
            try
            {
                byte[] bytIn = Convert.FromBase64String(Source);
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();
                ICryptoTransform encrypto = aes.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return sr.ReadToEnd();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region byte[]加密解密操作
        /// <summary>
        /// AES加密方法byte[] to byte[]（失败返回NULL）
        /// </summary>
        /// <param name="Source">明文</param>
        /// <returns>密文</returns>
        public byte[] Encrypt(byte[] Source)
        {
            try
            {
                byte[] bytIn = Source;
                MemoryStream ms = new MemoryStream();
                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();
                ICryptoTransform encrypto = aes.CreateEncryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Write);
                cs.Write(bytIn, 0, bytIn.Length);
                cs.FlushFinalBlock();
                ms.Close();
                byte[] bytOut = ms.ToArray();
                return bytOut;
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// AES解密方法byte[] to byte[]（失败返回NULL）
        /// </summary>
        /// <param name="Source">密文</param>
        /// <returns>明文</returns>
        public byte[] Decrypt(byte[] Source)
        {
            try
            {
                byte[] bytIn = Source;
                MemoryStream ms = new MemoryStream(bytIn, 0, bytIn.Length);
                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();
                ICryptoTransform encrypto = aes.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, encrypto, CryptoStreamMode.Read);
                StreamReader sr = new StreamReader(cs);
                return UTF8Encoding.UTF8.GetBytes(sr.ReadToEnd());
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 文件加密解密操作
        /// <summary>
        /// AES加密方法File to File（失败返回NULL）
        /// </summary>
        /// <param name="inFileName">待加密文件的路径</param>
        /// <param name="outFileName">待加密后文件的输出路径</param>
        public void Encrypt(string inFileName, string outFileName)
        {
            try
            {
                FileStream fin = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();

                byte[] bin = new byte[100];
                long rdlen = 0;
                long totlen = fin.Length;
                int len;

                ICryptoTransform encrypto = aes.CreateEncryptor();
                CryptoStream cs = new CryptoStream(fout, encrypto, CryptoStreamMode.Write);
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    cs.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }
                cs.Close();
                fout.Close();
                fin.Close();
            }
            catch
            {
                return;
            }
        }

        /// <summary>
        /// AES解密方法File to File（失败返回NULL）
        /// </summary>
        /// <param name="inFileName">待解密文件的路径</param>
        /// <param name="outFileName">待解密后文件的输出路径</param>
        public void Decrypt(string inFileName, string outFileName)
        {
            try
            {
                FileStream fin = new FileStream(inFileName, FileMode.Open, FileAccess.Read);
                FileStream fout = new FileStream(outFileName, FileMode.OpenOrCreate, FileAccess.Write);
                fout.SetLength(0);

                byte[] bin = new byte[100];
                long rdlen = 0;
                long totlen = fin.Length;
                int len;
                aes.Key = GetLegalKey();
                aes.IV = GetLegalIV();
                ICryptoTransform encrypto = aes.CreateDecryptor();
                CryptoStream cs = new CryptoStream(fout, encrypto, CryptoStreamMode.Write);
                while (rdlen < totlen)
                {
                    len = fin.Read(bin, 0, 100);
                    cs.Write(bin, 0, len);
                    rdlen = rdlen + len;
                }
                cs.Close();
                fout.Close();
                fin.Close();

            }
            catch
            {
                return;
            }
        }
        #endregion
    }
    #endregion

    #region RC2加密算法
    /// <summary>
    /// RC2加密算法基础类
    /// </summary>
    public class RC2
    {
        private string Key;
        private string IV;
        private RC2CryptoServiceProvider rc2;

        #region RC2算法构造函数
        /// <summary>
        /// RC2算法构造函数（KEY和IV随机生成）
        /// </summary>
        public RC2()
        {
            rc2 = new RC2CryptoServiceProvider();
            rc2.GenerateKey();
            rc2.GenerateIV();
            Key = Convert.ToBase64String(rc2.Key);
            IV = Convert.ToBase64String(rc2.IV);
        }
        /// <summary>
        /// RC2算法构造函数（KEY自定义，IV随机生成）
        /// </summary>
        /// <param name="sKey">8位KEY</param>
        public RC2(string sKey)
        {
            rc2 = new RC2CryptoServiceProvider();
            rc2.GenerateIV();
            Key = sKey;
            IV = Convert.ToBase64String(rc2.IV);
        }
        /// <summary>
        /// RC2算法构造函数（KEY和IV自定义）
        /// </summary>
        /// <param name="sKey">8位KEY</param>
        /// <param name="sIV">16位IV</param>
        public RC2(string sKey, string sIV)
        {
            rc2 = new RC2CryptoServiceProvider();
            Key = sKey;
            IV = sIV;
        }
        #endregion

        #region Get方法
        /// <summary>
        /// 获取KEY
        /// </summary>
        public string getKey
        {
            get
            {
                return Key;
            }
        }
        /// <summary>
        /// 获取IV
        /// </summary>
        public string getIV
        {
            get
            {
                return IV;
            }
        }
        #endregion

        #region 获取KEY和IV的私有方法
        /// <summary>
        /// 获得密钥
        /// </summary>
        /// <returns>密钥</returns>
        private byte[] GetLegalKey()
        {
            string sTemp = Key;
            rc2.GenerateKey();
            byte[] bytTemp = rc2.Key;
            int KeyLength = bytTemp.Length;
            if (sTemp.Length > KeyLength)
                sTemp = sTemp.Substring(0, KeyLength);
            else if (sTemp.Length < KeyLength)
                sTemp = sTemp.PadRight(KeyLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }

        /// <summary>
        /// 获得初始向量IV
        /// </summary>
        /// <returns>初试向量IV</returns>
        private byte[] GetLegalIV()
        {
            string sTemp = IV;
            rc2.GenerateIV();
            byte[] bytTemp = rc2.IV;
            int IVLength = bytTemp.Length;
            if (sTemp.Length > IVLength)
                sTemp = sTemp.Substring(0, IVLength);
            else if (sTemp.Length < IVLength)
                sTemp = sTemp.PadRight(IVLength, ' ');
            return ASCIIEncoding.ASCII.GetBytes(sTemp);
        }
        #endregion

        #region 加密与解密
        /// <summary>
        /// RC2加密算法（失败返回NULL）
        /// </summary>
        /// <param name="text">明文</param>
        /// <returns>密文</returns>
        public string Encrypt(string text)
        {
            try
            {
                MemoryStream ms = new MemoryStream();
                rc2.Key = GetLegalKey();
                rc2.IV = GetLegalIV();
                ICryptoTransform transform = rc2.CreateEncryptor();
                CryptoStream encstream = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                byte[] buffer = Encoding.UTF8.GetBytes(text); //加密明文时用Encoding.UTF8.
                encstream.Write(buffer, 0, buffer.Length);
                encstream.FlushFinalBlock();
                ms.Close();
                encstream.Clear();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// RC2解密算法（失败返回NULL）
        /// </summary>
        /// <param name="text">密文</param>
        /// <returns>明文</returns>
        public string Decrypt(string text)
        {
            try
            {
                byte[] buffer = Convert.FromBase64String(text); //解密密文时用Convert.FromBase64String
                MemoryStream ms = new MemoryStream();
                rc2.Key = GetLegalKey();
                rc2.IV = GetLegalIV();
                ICryptoTransform transform = rc2.CreateDecryptor();
                CryptoStream cs = new CryptoStream(ms, transform, CryptoStreamMode.Write);
                cs.Write(buffer, 0, buffer.Length);
                cs.FlushFinalBlock();
                ms.Close();
                cs.Clear();
                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
    #endregion

    #region RSA加密算法
    /// <summary>
    /// RSA加密算法基础类
    /// </summary>
    public class RSA
    {
        public RSA()
        { }

        #region RSA 的密钥产生
        /// <summary>
        /// RSA产生密钥
        /// </summary>
        /// <param name="xmlKeys">私钥</param>
        /// <param name="xmlPublicKey">公钥</param>
        public void RSAKey(out string xmlKeys, out string xmlPublicKey)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                xmlKeys = rsa.ToXmlString(true);
                xmlPublicKey = rsa.ToXmlString(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA加密函数
        //############################################################################## 
        //RSA 方式加密 
        //KEY必须是XML的形式,返回的是字符串 
        //该加密方式有长度限制的！
        //############################################################################## 

        /// <summary>
        /// RSA的加密函数
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="encryptString">待加密的字符串</param>
        /// <returns></returns>
        public string Encrypt(string xmlPublicKey, string encryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                PlainTextBArray = (new UnicodeEncoding()).GetBytes(encryptString);
                CypherTextBArray = rsa.Encrypt(PlainTextBArray, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的加密函数 
        /// </summary>
        /// <param name="xmlPublicKey">公钥</param>
        /// <param name="EncryptString">待加密的字节数组</param>
        /// <returns></returns>
        public string Encrypt(string xmlPublicKey, byte[] EncryptString)
        {
            try
            {
                byte[] CypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPublicKey);
                CypherTextBArray = rsa.Encrypt(EncryptString, false);
                Result = Convert.ToBase64String(CypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA的解密函数
        /// <summary>
        /// RSA的解密函数
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="decryptString">待解密的字符串</param>
        /// <returns></returns>
        public string Decrypt(string xmlPrivateKey, string decryptString)
        {
            try
            {
                byte[] PlainTextBArray;
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                PlainTextBArray = Convert.FromBase64String(decryptString);
                DypherTextBArray = rsa.Decrypt(PlainTextBArray, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA的解密函数 
        /// </summary>
        /// <param name="xmlPrivateKey">私钥</param>
        /// <param name="DecryptString">待解密的字节数组</param>
        /// <returns></returns>
        public string Decrypt(string xmlPrivateKey, byte[] DecryptString)
        {
            try
            {
                byte[] DypherTextBArray;
                string Result;
                System.Security.Cryptography.RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                rsa.FromXmlString(xmlPrivateKey);
                DypherTextBArray = rsa.Decrypt(DecryptString, false);
                Result = (new UnicodeEncoding()).GetString(DypherTextBArray);
                return Result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region 获取Hash描述表
        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="strSource">待签名的字符串</param>
        /// <param name="HashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(string strSource, ref byte[] HashData)
        {
            try
            {
                byte[] Buffer;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="strSource">待签名的字符串</param>
        /// <param name="strHashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(string strSource, ref string strHashData)
        {
            try
            {
                //从字符串中取得Hash描述 
                byte[] Buffer;
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                Buffer = System.Text.Encoding.GetEncoding("GB2312").GetBytes(strSource);
                HashData = MD5.ComputeHash(Buffer);
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="objFile">待签名的文件</param>
        /// <param name="HashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(System.IO.FileStream objFile, ref byte[] HashData)
        {
            try
            {
                //从文件中取得Hash描述 
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 获取Hash描述表
        /// </summary>
        /// <param name="objFile">待签名的文件</param>
        /// <param name="strHashData">Hash描述</param>
        /// <returns></returns>
        public bool GetHash(System.IO.FileStream objFile, ref string strHashData)
        {
            try
            {
                //从文件中取得Hash描述 
                byte[] HashData;
                System.Security.Cryptography.HashAlgorithm MD5 = System.Security.Cryptography.HashAlgorithm.Create("MD5");
                HashData = MD5.ComputeHash(objFile);
                objFile.Close();
                strHashData = Convert.ToBase64String(HashData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA签名
        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="HashbyteSignature">待签名Hash描述</param>
        /// <param name="EncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="HashbyteSignature">待签名Hash描述</param>
        /// <param name="m_strEncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, byte[] HashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] EncryptedSignatureData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="strHashbyteSignature">待签名Hash描述</param>
        /// <param name="EncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref byte[] EncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;

                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();

                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// RSA签名
        /// </summary>
        /// <param name="strKeyPrivate">私钥</param>
        /// <param name="strHashbyteSignature">待签名Hash描述</param>
        /// <param name="strEncryptedSignatureData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureFormatter(string strKeyPrivate, string strHashbyteSignature, ref string strEncryptedSignatureData)
        {
            try
            {
                byte[] HashbyteSignature;
                byte[] EncryptedSignatureData;
                HashbyteSignature = Convert.FromBase64String(strHashbyteSignature);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPrivate);
                System.Security.Cryptography.RSAPKCS1SignatureFormatter RSAFormatter = new System.Security.Cryptography.RSAPKCS1SignatureFormatter(RSA);
                //设置签名的算法为MD5 
                RSAFormatter.SetHashAlgorithm("MD5");
                //执行签名 
                EncryptedSignatureData = RSAFormatter.CreateSignature(HashbyteSignature);
                strEncryptedSignatureData = Convert.ToBase64String(EncryptedSignatureData);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region RSA 签名验证
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="HashbyteDeformatter">Hash描述</param>
        /// <param name="DeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="strHashbyteDeformatter">Hash描述</param>
        /// <param name="DeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, byte[] DeformatterData)
        {
            try
            {
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="HashbyteDeformatter">Hash描述</param>
        /// <param name="strDeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, byte[] HashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// RSA签名验证
        /// </summary>
        /// <param name="strKeyPublic">公钥</param>
        /// <param name="strHashbyteDeformatter">Hash描述</param>
        /// <param name="strDeformatterData">签名后的结果</param>
        /// <returns></returns>
        public bool SignatureDeformatter(string strKeyPublic, string strHashbyteDeformatter, string strDeformatterData)
        {
            try
            {
                byte[] DeformatterData;
                byte[] HashbyteDeformatter;
                HashbyteDeformatter = Convert.FromBase64String(strHashbyteDeformatter);
                System.Security.Cryptography.RSACryptoServiceProvider RSA = new System.Security.Cryptography.RSACryptoServiceProvider();
                RSA.FromXmlString(strKeyPublic);
                System.Security.Cryptography.RSAPKCS1SignatureDeformatter RSADeformatter = new System.Security.Cryptography.RSAPKCS1SignatureDeformatter(RSA);
                //指定解密的时候HASH算法为MD5 
                RSADeformatter.SetHashAlgorithm("MD5");
                DeformatterData = Convert.FromBase64String(strDeformatterData);
                if (RSADeformatter.VerifySignature(HashbyteDeformatter, DeformatterData))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
    #endregion

    #region 摘要算法
    /// <summary>
    /// 摘要算法抽象基础类
    /// </summary>
    public abstract class Abstract
    {
        public Abstract()
        { }

        #region MD5
        /// <summary>
        /// MD5加密
        /// </summary>
        /// <param name="message">待加密字符串</param>
        /// <returns>加密字符串</returns>
        public static string MD5(string Input)
        {
            string output = null;
            if (string.IsNullOrEmpty(Input))
            {
                output = Input;
            }
            else
            {
                output = System.Security.Cryptography.MD5.Create(Input).Hash.ToString();
            }
            return output;
        }
        #endregion

        #region SHA1
        /// <summary>
        /// SHA1加密
        /// </summary>
        /// <param name="message">待加密字符串</param>
        /// <returns>加密字符串</returns>
        public static string SHA1(string Input)
        {
            string output = null;
            if (string.IsNullOrEmpty(Input))
            {
                output = Input;
            }
            else
            {
                output = System.Security.Cryptography.SHA1.Create(Input).Hash.ToString();
            }
            return output;
        }
        #endregion
    }
    #endregion
}
