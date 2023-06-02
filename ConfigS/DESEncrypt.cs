using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace WebApi.Models
{
    // 암호화 처리를 위한 로직 암호화, 복호화 
    // 사용안함 서버에서 암호화 할일 없음 
    // 클라이언트에서 라이브러리로 활용 
    public class DESEncrypt
    {
        // 2016.04.22 romee GS 인증 권고사항 
        public static string EncryptSHA(string Data)
        {
            string hash1 = Hash(Data);
            //string aes1 = EncryptAES(hash1);
            return hash1;
        }
        //Encrypt

        // 2016.04.22 romee GS 인증 권고사항 
        public static string DecryptSHA(string Data, string hashText)
        {
            return isHashEqual(Data, hashText);
        }
        //Decrypt


        // 2016.04.22 romee GS 인증 권고사항 
        public static string EncryptAES(string Data)
        {
            return Encrypt(Data, "i2ms2");
        }
        //Encrypt

        // 2016.04.22 romee GS 인증 권고사항 
        public static string DecryptAES(string Data)
        {
            return Decrypt(Data, "i2ms2");
        }
        //Decrypt

        // 2016.04.22 romee GS 인증 권고사항 
        public static string EncryptSHA_AES(string Data)
        {
            string data2 = EncryptSHA(Data);
            return Encrypt(data2, data2.Substring(0, 5));
        }
        //Encrypt

        // 2016.04.22 romee GS 인증 권고사항 
        public static string DecryptSHA_AES(string Data, string pwd)
        {
            string data2 = EncryptSHA(pwd);

            string data3 = Decrypt(Data, data2.Substring(0, 5));

            if (data2 == data3)
                return Data;
            return null;
        }



        #region GS 인증 해쉬 알고리즘 SHA256
        /// <summary>
        /// 입력 텍스트를 해슁 알고리즘으로 암호화한다(해슁 알고리즘 MD5)
        /// </summary>
        /// <param name="inputText">암호화할 텍스트</param>
        /// <returns>암호화된 텍스트</returns>
        public static string Hash(String inputText)
        {
            return ComputeHash(inputText);
        }

        /// <summary>
        /// 입력 텍스트와 해쉬된 텍스트가 같은지 여부를 비교한다.
        /// </summary>
        /// <param name="inputText">해쉬되지 않은 입력 텍스트</param>
        /// <param name="hashText">해쉬된 텍스트</param>
        /// <returns>비교 결과</returns>
        public static string isHashEqual(string inputText, string hashText)
        {
            if (Hash(inputText) == hashText)
                return inputText;
            else
                return "";

        }

        /// <summary>
        /// 해쉬 코드를 계산해서 스트링으로 변환함
        /// </summary>
        /// <param name="inputText">해쉬코드로 변환할 스트링</param>
        /// <param name="hashingType">사용할 해슁 타입</param>
        /// <returns>hashed string</returns>
        private static string ComputeHash(string inputText)
        {
            SHA256Managed HA = new SHA256Managed();

            string hash = String.Empty;
            byte[] crypto = HA.ComputeHash(Encoding.ASCII.GetBytes(inputText), 0, Encoding.ASCII.GetByteCount(inputText));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash;
        }
        #endregion

        #region GS 인증 DES 알고리즘 SHA256
        /*
        private static byte[] pbyteKey = ASCIIEncoding.ASCII.GetBytes("Jake_Key");

        public static string EncryptDES(String strKey)
        {
            string strReturn = "";
            try
            {
                DESCryptoServiceProvider desCSP = new DESCryptoServiceProvider();
                desCSP.Mode = CipherMode.ECB;
                desCSP.Padding = PaddingMode.PKCS7;
                desCSP.Key = pbyteKey;
                desCSP.IV = pbyteKey;
                MemoryStream ms = new MemoryStream();
                CryptoStream cryptStream = new CryptoStream(ms, desCSP.CreateEncryptor(), CryptoStreamMode.Write);
                byte[] data = Encoding.UTF8.GetBytes(strKey.ToCharArray());
                cryptStream.Write(data, 0, data.Length);
                cryptStream.FlushFinalBlock();

                strReturn = Convert.ToBase64String(ms.ToArray());
                cryptStream = null;
                ms = null;
                desCSP = null;
            }
            catch (Exception)
            { }
            return strReturn;
        }


        public static string DecryptDES(String strKey)
        {
            string strReturn = "";
            try
            {
                DESCryptoServiceProvider desCSP = new DESCryptoServiceProvider();
                desCSP.Mode = CipherMode.ECB;
                desCSP.Padding = PaddingMode.PKCS7;
                desCSP.Key = pbyteKey;
                desCSP.IV = pbyteKey;
                MemoryStream ms = new MemoryStream();
                CryptoStream cryptStream = new CryptoStream(ms, desCSP.CreateDecryptor(), CryptoStreamMode.Write);
                strKey = strKey.Replace(" ", "+");
                byte[] data = Convert.FromBase64String(strKey);
                cryptStream.Write(data, 0, data.Length);
                cryptStream.FlushFinalBlock();

                string strData = Encoding.UTF8.GetString(ms.GetBuffer());
                int pos = strData.IndexOf((char)0);
                strReturn = pos == -1 ? strData : strData.Substring(0, pos);

                cryptStream = null;
                ms = null;
                desCSP = null;
            }
            catch (Exception)
            { }
            return strReturn;
        }
         */
        #endregion

        #region GS 인증 AES 알고리즘 SHA256

        public static byte[] AES_Encrypt(byte[] bytesToBeEncrypted, byte[] passwordBytes)
        {
            byte[] encryptedBytes = null;

            // Set your salt here, change it to meet your flavor:  
            byte[] saltBytes = passwordBytes;
            // Example:  
            //saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };  

            using (MemoryStream ms = new MemoryStream())
            {
                using (RijndaelManaged AES = new RijndaelManaged())
                {
                    AES.KeySize = 256;
                    AES.BlockSize = 128;

                    var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                    AES.Key = key.GetBytes(AES.KeySize / 8);
                    AES.IV = key.GetBytes(AES.BlockSize / 8);

                    AES.Mode = CipherMode.CBC;

                    using (CryptoStream cs = new CryptoStream(ms, AES.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(bytesToBeEncrypted, 0, bytesToBeEncrypted.Length);
                        cs.Close();
                    }
                    encryptedBytes = ms.ToArray();
                }
            }

            return encryptedBytes;
        }
        public static byte[] AES_Decrypt(byte[] bytesToBeDecrypted, byte[] passwordBytes)
        {
            try
            {
                byte[] decryptedBytes = null;
                // Set your salt here to meet your flavor:  
                byte[] saltBytes = passwordBytes;
                // Example:  
                //saltBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };  

                using (MemoryStream ms = new MemoryStream())
                {
                    using (RijndaelManaged AES = new RijndaelManaged())
                    {
                        AES.KeySize = 256;
                        AES.BlockSize = 128;

                        var key = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 1000);
                        AES.Key = key.GetBytes(AES.KeySize / 8);
                        AES.IV = key.GetBytes(AES.BlockSize / 8);

                        //AES.Mode = CipherMode.CBC;  

                        using (CryptoStream cs = new CryptoStream(ms, AES.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(bytesToBeDecrypted, 0, bytesToBeDecrypted.Length);
                            //If(cs.Length = ""  
                            cs.Close();
                        }
                        decryptedBytes = ms.ToArray();
                    }
                }
                return decryptedBytes;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public static string Encrypt(string text, string pwd)
        {
            byte[] originalBytes = Encoding.UTF8.GetBytes(text);
            byte[] encryptedBytes = null;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

            // Hash the password with SHA256  
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            // Getting the salt size  
            int saltSize = GetSaltSize(passwordBytes);
            // Generating salt bytes  
            byte[] saltBytes = GetRandomBytes(saltSize);

            // Appending salt bytes to original bytes  
            byte[] bytesToBeEncrypted = new byte[saltBytes.Length + originalBytes.Length];
            for (int i = 0; i < saltBytes.Length; i++)
            {
                bytesToBeEncrypted[i] = saltBytes[i];
            }
            for (int i = 0; i < originalBytes.Length; i++)
            {
                bytesToBeEncrypted[i + saltBytes.Length] = originalBytes[i];
            }

            encryptedBytes = AES_Encrypt(bytesToBeEncrypted, passwordBytes);

            return Convert.ToBase64String(encryptedBytes);
        }

        public static string Decrypt(string decryptedText, string pwd)
        {
            byte[] bytesToBeDecrypted = Convert.FromBase64String(decryptedText);
            byte[] passwordBytes = Encoding.UTF8.GetBytes(pwd);

            // Hash the password with SHA256  
            passwordBytes = SHA256.Create().ComputeHash(passwordBytes);

            byte[] decryptedBytes = AES_Decrypt(bytesToBeDecrypted, passwordBytes);

            if (decryptedBytes != null)
            {
                // Getting the size of salt  
                int saltSize = GetSaltSize(passwordBytes);

                // Removing salt bytes, retrieving original bytes  
                byte[] originalBytes = new byte[decryptedBytes.Length - saltSize];
                for (int i = saltSize; i < decryptedBytes.Length; i++)
                {
                    originalBytes[i - saltSize] = decryptedBytes[i];
                }
                return Encoding.UTF8.GetString(originalBytes);
            }
            else
            {
                return null;
            }
        }
        private static int GetSaltSize(byte[] passwordBytes)
        {
            var key = new Rfc2898DeriveBytes(passwordBytes, passwordBytes, 1000);
            byte[] ba = key.GetBytes(2);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ba.Length; i++)
            {
                sb.Append(Convert.ToInt32(ba[i]).ToString());
            }
            int saltSize = 0;
            string s = sb.ToString();
            foreach (char c in s)
            {
                int intc = Convert.ToInt32(c.ToString());
                saltSize = saltSize + intc;
            }

            return saltSize;
        }
        public static byte[] GetRandomBytes(int length)
        {
            byte[] ba = new byte[length];
            RNGCryptoServiceProvider.Create().GetBytes(ba);
            return ba;
        }
        #endregion

    }

}