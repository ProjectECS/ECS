using System;
using System.Text;

namespace ChiaraMail
{
    class AES_JS
    {
        /*
         *  ported from jsaes version 0.1  -  Copyright 2006 B. Poettering
         */

        /*
         * http://point-at-infinity.org/jsaes/
         *
         * This is a javascript implementation of the AES block cipher. Key lengths 
         * of 128, 192 and 256 bits are supported.        
         */

        /******************************************************************************/

        /* 
           AES_Init: initialize the tables needed at runtime. Call this function
           before the (first) key expansion.
        */

        static int[] AES_xtime;
        static int[] AES_Sbox_Inv;
        static int[] AES_ShiftRowTab_Inv;
 
        private static void Initialize() {
          AES_Sbox_Inv = new int[256];
          for(var i = 0; i < 256; i++)
            AES_Sbox_Inv[AES_Sbox[i]] = i;
  
          AES_ShiftRowTab_Inv = new int[16];
          for(var i = 0; i < 16; i++)
            AES_ShiftRowTab_Inv[AES_ShiftRowTab[i]] = (byte)i;

          AES_xtime = new int[256];
          for(var i = 0; i < 128; i++) {
            AES_xtime[i] = (i << 1);
            AES_xtime[128 + i] = (i << 1) ^ 0x1b;
          }
        }

        /* 
           AES_Done: release memory reserved by AES_Init. Call this function after
           the last encryption/decryption operation.
        */

        private static void Cleanup() {
          AES_Sbox_Inv = null;
          AES_ShiftRowTab_Inv = null;
          AES_xtime = null;
        }

        /*
           AES_ExpandKey: expand a cipher key. Depending on the desired encryption 
           strength of 128, 192 or 256 bits 'key' has to be a byte array of length 
           16, 24 or 32, respectively. The key expansion is done "in place", meaning 
           that the array 'key' is modified.
        */

        private static void ExpandKey(ref byte[] key) 
        {
            var kl = key.Length;
            int ks;
            int Rcon = 1;
            switch (kl) 
            {
                case 16: ks = 16 * (10 + 1); break;
                case 24: ks = 16 * (12 + 1); break;
                case 32: ks = 16 * (14 + 1); break;
                default: 
                  throw new ArgumentOutOfRangeException("AES_ExpandKey: Only key lengths of 16, 24 or 32 bytes allowed!");
            }
            //expand array to new size
            var newKey = new byte[ks];
            key.CopyTo(newKey, 0);
            key = newKey;
            byte[] temp = new byte[4];
            for (var i = kl; i < ks; i += 4) 
            {
                Array.Copy(key, i - 4, temp, 0, 4);
                //var temp = key.slice(i - 4, i);
                if (i % kl == 0) 
                {
                    byte holder = temp[0];
                    temp[0] = Convert.ToByte(AES_Sbox[temp[1]] ^ Rcon);
                    temp[1] = (byte)AES_Sbox[temp[2]];
                    temp[2] = (byte)AES_Sbox[temp[3]];
                    temp[3] = (byte)AES_Sbox[holder]; 
                    if ((Rcon <<= 1) >= 256) Rcon ^= 0x11b;
                }
                else if ((kl > 24) && (i % kl == 16))
                {
                    temp[0] = (byte)AES_Sbox[temp[0]];
                    temp[1] = (byte)AES_Sbox[temp[1]];
                    temp[2] = (byte)AES_Sbox[temp[2]];
                    temp[3] = (byte)AES_Sbox[temp[3]];
                }
                for(var j = 0; j < 4; j++)                    
                    key[i + j] = Convert.ToByte(key[i + j - kl] ^ temp[j]);
            }
        }

        /* 
           Encrypt: encrypt the 16 byte array 'block' with the previously 
           expanded key 'key'.
        */
        internal static byte[] Encrypt(string content, string key)
        {
            try
            {
                //get the bytes
                byte[] block = Encoding.UTF8.GetBytes(content);
                Encrypt(ref block, Encoding.UTF8.GetBytes(key));
                return block;
            }
            catch (Exception ex)
            {
                Logger.Error("AES_JS.Encrypt", ex.ToString());
            }
            return null;
        }

        internal static byte[] Encrypt(byte[] block, string key)
        {
            try
            {
                Encrypt(ref block, Encoding.UTF8.GetBytes(key));
            }
            catch (Exception ex)
            {
                Logger.Error("AES_JS.Encrypt", ex.ToString());
            }
            return block;
        }
        
        private static void Encrypt(ref byte[] block, byte[] key) 
        {
            Initialize();
            ExpandKey(ref key);
            var l = key.Length;
            byte[]temp = new byte[16];
            //resize block to even multiplier of 16            
            if (block.Length % 16 > 0)
            {
                Resize(ref block, ((block.Length / 16) + 1) * 16);
            }
            var m = block.Length;
            //encrypt block in chunks of 16 bytes
            for (int j = 0; j < block.Length; j += 16)
            {
                byte[] chunk = new byte[16];
                Array.Copy(block, j, chunk, 0, m - j >= 16 ? 16 : m - j);
                Array.Copy(key, 0, temp, 0, 16);
                AES_AddRoundKey(ref chunk, key);
                int i;
                for (i = 16; i < l - 16; i += 16)
                {
                    AES_SubBytes(ref chunk, AES_Sbox);
                    AES_ShiftRows(ref chunk, AES_ShiftRowTab);
                    AES_MixColumns(ref chunk);
                    Array.Copy(key, i, temp, 0, 16);
                    AES_AddRoundKey(ref chunk, temp);
                }
                AES_SubBytes(ref chunk, AES_Sbox);
                AES_ShiftRows(ref chunk, AES_ShiftRowTab);
                Array.Copy(key, i, temp, 0, 16);
                AES_AddRoundKey(ref chunk, temp);
                //copy encrypted chunk back to block
                Array.Copy(chunk, 0, block, j, m - j >= 16 ? 16 : m - j);
            }
            Cleanup();
        }

        /* 
           AES_Decrypt: decrypt the 16 byte array 'block' with the previously 
           expanded key 'key'.
        */
        //internal static string Decrypt(byte[] block, string key)
        //{
        //    try
        //    {
        //        //get the bytes
        //        //byte[] block = Encoding.UTF8.GetBytes(content);
        //        Decrypt(ref block, Encoding.UTF8.GetBytes(key));
        //        return Encoding.UTF8.GetString(block);
        //    }
        //    catch (Exception ex)
        //    {
        //        Logger.Error("AES-JS.Decrypt", ex.ToString());
        //    }
        //    return "";
        //}

        internal static byte[] Decrypt(byte[] block, string key)
        {
            try
            {
                Decrypt(ref block,Encoding.UTF8.GetBytes(key));
            }
            catch (Exception ex)
            {
                Logger.Error("AES_JS.Decrypt",ex.ToString());
            }
            return block;
        }

        private static void Decrypt(ref byte[] block, byte[] key) 
        {
            Initialize();
            ExpandKey(ref key);
            var l = key.Length;
            byte[] temp = new byte[16];
            //resize block to even multiplier of 16            
            if (block.Length % 16 > 0)
            {
                Resize(ref block, ((block.Length / 16) + 1) * 16);
            }
            var m = block.Length;
            //decrypt block in chunks of 16 bytes
            for (int j = 0; j < block.Length; j += 16)
            {
                byte[] chunk = new byte[16];
                Array.Copy(block, j, chunk, 0, m - j >= 16 ? 16 : m - j);
                Array.Copy(key, l - 16, temp, 0, 16);
                AES_AddRoundKey(ref chunk, temp);
                AES_ShiftRows(ref chunk, AES_ShiftRowTab_Inv);
                AES_SubBytes(ref  chunk, AES_Sbox_Inv);
                for (var i = l - 32; i >= 16; i -= 16)
                {
                    Array.Copy(key, i, temp, 0, 16);
                    AES_AddRoundKey(ref chunk, temp);
                    AES_MixColumns_Inv(ref chunk);
                    AES_ShiftRows(ref chunk, AES_ShiftRowTab_Inv);
                    AES_SubBytes(ref chunk, AES_Sbox_Inv);
                }
                Array.Copy(key, 0, temp, 0, 16);
                AES_AddRoundKey(ref chunk, temp);
                //copy decrypted bytes back to block
                Array.Copy(chunk, 0, block, j, m - j >= 16 ? 16 : m - j);
            }
            /*
             * we may have padded the original array with some empty (0) trailing bytes
             * more than 4 of those will corrupt some files (like .docx)
             * so strip them off
            */
            var count = 0;
            for (int i = block.Length - 1; i > -1; i--)
            {
                if (block[i] != 0) break;
                count++;
            }
            if (count > 4) 
            {
                //shrink it
                byte[] shrink = new byte[block.Length - (count-4)];
                for(int i=0;i<shrink.Length;i++)
                {
                    shrink[i]=block[i];
                }
                block = shrink;
            }
        }

        /******************************************************************************/

        //internal lookup tables and functions 
        static int[] AES_Sbox ={99,124,119,123,242,107,111,197,48,1,103,43,254,215,171,
          118,202,130,201,125,250,89,71,240,173,212,162,175,156,164,114,192,183,253,
          147,38,54,63,247,204,52,165,229,241,113,216,49,21,4,199,35,195,24,150,5,154,
          7,18,128,226,235,39,178,117,9,131,44,26,27,110,90,160,82,59,214,179,41,227,
          47,132,83,209,0,237,32,252,177,91,106,203,190,57,74,76,88,207,208,239,170,
          251,67,77,51,133,69,249,2,127,80,60,159,168,81,163,64,143,146,157,56,245,
          188,182,218,33,16,255,243,210,205,12,19,236,95,151,68,23,196,167,126,61,
          100,93,25,115,96,129,79,220,34,42,144,136,70,238,184,20,222,94,11,219,224,
          50,58,10,73,6,36,92,194,211,172,98,145,149,228,121,231,200,55,109,141,213,
          78,169,108,86,244,234,101,122,174,8,186,120,37,46,28,166,180,198,232,221,
          116,31,75,189,139,138,112,62,181,102,72,3,246,14,97,53,87,185,134,193,29,
          158,225,248,152,17,105,217,142,148,155,30,135,233,206,85,40,223,140,161,
          137,13,191,230,66,104,65,153,45,15,176,84,187,22};

        static int[] AES_ShiftRowTab = {0,5,10,15,4,9,14,3,8,13,2,7,12,1,6,11};

        private static void Resize(ref byte[] target, int size)
        {
            if (target.Length < size)
            {
                byte[] temp = new byte[size];
                target.CopyTo(temp, 0);
                target = temp;
            }
        }
        private static void AES_SubBytes(ref byte[]state, int[] sbox) 
        {
            Resize(ref state, 16);
            for(var i = 0; i < 16; i++)
                state[i] = (byte)sbox[state[i]];  
        }

        private static void AES_AddRoundKey(ref byte[]state, byte[] rkey) 
        {
            Resize(ref state, 16);
            for(var i = 0; i < 16; i++)
                state[i] ^= rkey[i];
        }

        private static void AES_ShiftRows(ref byte[] state, int[] shifttab) 
        {
            Resize(ref state, 16);
            var h = new byte[state.Length];
            state.CopyTo(h,0);
            for(var i = 0; i < 16; i++)
                state[i] = h[shifttab[i]];
        }

        private static void AES_MixColumns(ref byte[] state) 
        {
            Resize(ref state, 16);
            for(var i = 0; i < 16; i += 4) 
            {
                var s0 = state[i + 0];
                var s1 = state[i + 1];
                var s2 = state[i + 2];
                var s3 = state[i + 3];
                var h = s0 ^ s1 ^ s2 ^ s3;
                state[i + 0] ^= Convert.ToByte(h ^ AES_xtime[s0 ^ s1]);
                state[i + 1] ^= Convert.ToByte(h ^ AES_xtime[s1 ^ s2]);
                state[i + 2] ^= Convert.ToByte(h ^ AES_xtime[s2 ^ s3]);
                state[i + 3] ^= Convert.ToByte(h ^ AES_xtime[s3 ^ s0]);
            }
        }

        private static void AES_MixColumns_Inv(ref byte[] state) 
        {
            Resize(ref state, 16);
            for(var i = 0; i < 16; i += 4) 
            {
                var s0 = state[i + 0];
                var s1 = state[i + 1];
                var s2 = state[i + 2];
                var s3 = state[i + 3];
                var h = s0 ^ s1 ^ s2 ^ s3;
                var xh = AES_xtime[h];
                var h1 = AES_xtime[AES_xtime[xh ^ s0 ^ s2]] ^ h;
                var h2 = AES_xtime[AES_xtime[xh ^ s1 ^ s3]] ^ h;
                state[i + 0] ^= Convert.ToByte(h1 ^ AES_xtime[s0 ^ s1]);
                state[i + 1] ^= Convert.ToByte(h2 ^ AES_xtime[s1 ^ s2]);
                state[i + 2] ^= Convert.ToByte(h1 ^ AES_xtime[s2 ^ s3]);
                state[i + 3] ^= Convert.ToByte(h2 ^ AES_xtime[s3 ^ s0]);
            }
        }

    }
}
