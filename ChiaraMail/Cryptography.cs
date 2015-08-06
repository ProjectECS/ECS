using System;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace ChiaraMail
{
    internal class Cryptography
    {
        private static string _className = "Cryptography";
    
        #region Public methods

        internal static string EncryptAES(string input, string seed)
        {
            string source = _className + "EncryptAES";
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    Logger.Warning(source, "missing input string");
                    return result;
                }
                if (string.IsNullOrEmpty(seed))
                {
                    Logger.Warning(source, "missing seed string");
                    return result;
                }
                byte[] salt = Encoding.UTF8.GetBytes(seed);
                byte[] raw = Encoding.UTF8.GetBytes(input);
                byte[] encrypted = EncryptSymmetric(raw, salt);
                result = Convert.ToBase64String(encrypted);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;

        }

        internal static byte[] EncryptAES(byte[] raw, string seed)
        {
            string source = _className + "EncryptAES";
            byte[] result = null;
            try
            {
                if (raw==null || raw.Length.Equals(0))
                {
                    Logger.Warning(source, "missing input bytes");
                    return null;
                }
                if (string.IsNullOrEmpty(seed))
                {
                    Logger.Warning(source, "missing seed string");
                    return null;
                }
                var salt = Encoding.UTF8.GetBytes(seed);
                result = EncryptSymmetric(raw, salt);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;

        }

        internal static string DecryptAES(string input, string seed)
        {
            string source = _className + "DecryptAES";
            string result = "";
            try
            {
                if (string.IsNullOrEmpty(input))
                {
                    Logger.Warning(source, "missing input string");
                    return result;
                }
                if (string.IsNullOrEmpty(seed))
                {
                    Logger.Warning(source, "missing seed string");
                    return result;
                }
                byte[] salt = Encoding.UTF8.GetBytes(seed);
                byte[] raw = Convert.FromBase64String(input);
                byte[] decrypted = DecryptSymmetric(raw, salt);
                result = Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;
        }

        internal static byte[] DecryptAES(byte[] raw, string seed)
        {
            string source = _className + "DecryptAES";
            byte[] result = null;
            try
            {
                if (raw == null || raw.Length.Equals(0))
                {
                    Logger.Warning(source, "missing input bytes");
                    return null;
                }
                if (string.IsNullOrEmpty(seed))
                {
                    Logger.Warning(source, "missing seed string");
                    return null;
                }
                byte[] salt = Encoding.UTF8.GetBytes(seed);
                result = DecryptSymmetric(raw, salt);
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;
        }

        internal static string GenerateKey(string salt)
        {
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(salt));
        }

        internal static string GenerateKey()
        {
            //generate 256-bit/32-byte key
            return Guid.NewGuid().ToString().Replace("-", "");
        }
        
        internal static string GetHash(byte[] data)
        {
            try
            {
                var md5 = new MD5CryptoServiceProvider();
                return BitConverter.ToString(md5.ComputeHash(data));
            }
            catch (Exception ex)
            {
                Logger.Error("GetHash", ex.ToString());
            }
            return "";
        }
#endregion

        #region Private methods
        
        private static byte[] EncryptSymmetric(byte[] input, byte[] salt)
        {
            string source = _className + "EncryptSymmetric";
            byte[] result = null;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (Aes aes = new AesManaged())
                    {
                        var key = new byte[aes.KeySize / 8];
                        var iv = new byte[aes.BlockSize / 8];
                        GenerateKeyAndIv(salt, ref key, ref iv);
                        aes.Key = key;
                        aes.IV = iv;
                        using (var cs = new CryptoStream(
                            ms, aes.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(input, 0, input.Length);
                            cs.FlushFinalBlock();
                        }
                        result = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;
        }

        private static byte[] DecryptSymmetric(byte[] input, byte[] salt)
        {
            string source = _className + "DecryptSymmetric";
            byte[] result = null;
            try
            {
                using (var ms = new MemoryStream())
                {
                    using (Aes aes = new AesManaged())
                    {
                        var key = new byte[aes.KeySize / 8];
                        var iv = new byte[aes.BlockSize / 8];
                        GenerateKeyAndIv(salt, ref key, ref iv);
                        aes.Key = key;
                        aes.IV = iv;
                        using (var cs = new CryptoStream(
                            ms, aes.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(input, 0, input.Length);
                            cs.FlushFinalBlock();
                        }
                        result = ms.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(source, ex.ToString());
            }
            return result;
        }

        private static void GenerateKeyAndIv(byte[] salt, ref byte[] key, ref byte[] iv)
        {
            for (int i = 0; i < key.Length; i++)
            {
                //salt may not be as large as keysize
                //if not then repeat it
                key[i] = salt[i % (salt.Length - 1)];
            }
            //reverse it
            byte[] temp = key;
            Array.Reverse(temp);
            //size may be different
            for (int i = 0; i < iv.Length; i++)
            {
                iv[i] = temp[i % (temp.Length - 1)];
            }
        }

        #endregion
    }
}
