using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    /// <summary>
    /// DES加密工具类
    /// </summary>
    public static class DESTools
    {
        /// <summary>
        /// DES加密
        /// </summary>
        /// <param name="text"></param>
        /// <param name="key">秘钥，长度超过8位取前面8个字符作为秘钥</param>
        /// <returns></returns>
        public static string Encrypt(string text, string key)
        {
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            try
            {
                var des = new DESCryptoServiceProvider();
                des.Mode = CipherMode.CBC;
                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(key);
                var inputByteArray = Encoding.GetEncoding("UTF-8").GetBytes(text);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                var ret = new StringBuilder();
                foreach (var b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b); //将第一个参数转换为十六进制数,长度为2,不足前面补0
                }
                return ret.ToString();
            }
            catch
            {

            }
            return string.Empty;
        }
        /// <summary>
        /// DES解密
        /// </summary>
        /// <param name="cyphertext"></param>
        /// <param name="key">秘钥，长度超过8位取前面8个字符作为秘钥</param>
        /// <returns></returns>
        public static string Decrypt(string cyphertext, string key)
        {
            if (key.Length > 8)
            {
                key = key.Substring(0, 8);
            }
            try
            {
                if (string.IsNullOrEmpty(cyphertext))
                    return string.Empty;
                var des = new DESCryptoServiceProvider();
                var inputByteArray = new byte[cyphertext.Length / 2];
                for (var x = 0; x < cyphertext.Length / 2; x++)
                {
                    var i = (Convert.ToInt32(cyphertext.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = Encoding.ASCII.GetBytes(key);
                des.IV = Encoding.ASCII.GetBytes(key);
                var ms = new MemoryStream();
                var cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Encoding.GetEncoding("UTF-8").GetString(ms.ToArray());
            }
            catch
            {

            }
            return string.Empty;
        }
    }
    /// <summary>
    /// 与页面Js的Des加密相匹配的C#Des加解密
    /// </summary>
    public static class JsDes
    {
        #region 加密解密及相关方法

        #region 配合JS用的C#版DES加解密方法及相关函数

        /// <summary>
        /// 加密测试函数
        /// </summary>
        /// <param name="beinetstr">待加密的字符串</param>
        /// <param name="beinetkey">密钥</param>
        /// <returns></returns>
        public static string EncMe(string beinetstr, string beinetkey)
        {
            if (string.IsNullOrEmpty(beinetkey))
                return string.Empty;

            return stringToHex(des(beinetkey, beinetstr, true, false, string.Empty));
        }

        /// <summary>
        /// 解密测试函数
        /// </summary>
        /// <param name="beinetstr">待解密的字符串</param>
        /// <param name="beinetkey">密钥</param>
        /// <returns></returns>
        public static string UncMe(string beinetstr, string beinetkey)
        {
            if (string.IsNullOrEmpty(beinetkey))
                return null;
            string ret = des(beinetkey, HexTostring(beinetstr), false, false, string.Empty);
            return ret;
        }

        /// <summary>
        /// 把字符串转换为16进制字符串
        /// 如：a变成61（即10进制的97）；abc变成616263
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string stringToHex(string s)
        {
            string r = "";
            string[] hexes = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "a", "b", "c", "d", "e", "f" };
            for (int i = 0; i < (s.Length); i++)
            {
                r += hexes[RM(s[i], 4)] + hexes[s[i] & 0xf];
            }
            return r;
        }

        /// <summary>
        /// 16进制字符串转换为字符串
        /// 如：61（即10进制的97）变成a；616263变成abc
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string HexTostring(string s)
        {
            string ret = string.Empty;

            for (int i = 0; i < s.Length; i += 2)
            {
                int sxx = Convert.ToInt32(s.Substring(i, 2), 16);
                ret += (char)sxx;
            }
            return ret;
        }

        /// <summary>
        /// 带符号位右移（类似于js的>>>）
        /// </summary>
        /// <param name="a">用于右移的操作数</param>
        /// <param name="bit">右移位数</param>
        /// <returns></returns>
        public static int RM(int a, int bit)
        {
            unchecked
            {
                uint b = (uint)a;
                b = b >> bit;
                return (int)b;
            }
        }

        /// <summary>
        /// 加解密主调方法
        /// </summary>
        /// <param name="beinetkey">密钥</param>
        /// <param name="message">加密时为string，解密时为byte[]</param>
        /// <param name="encrypt">true：加密；false：解密</param>
        /// <param name="mode">true：CBC mode；false：非CBC mode</param>
        /// <param name="iv">初始化向量</param>
        /// <returns></returns>
        public static string des(string beinetkey, string message, bool encrypt, bool mode, string iv)
        {
            //declaring this locally speeds things up a bit
            long[] spfunction1 = { 0x1010400, 0, 0x10000, 0x1010404, 0x1010004, 0x10404, 0x4, 0x10000, 0x400, 0x1010400, 0x1010404, 0x400, 0x1000404, 0x1010004, 0x1000000, 0x4, 0x404, 0x1000400, 0x1000400, 0x10400, 0x10400, 0x1010000, 0x1010000, 0x1000404, 0x10004, 0x1000004, 0x1000004, 0x10004, 0, 0x404, 0x10404, 0x1000000, 0x10000, 0x1010404, 0x4, 0x1010000, 0x1010400, 0x1000000, 0x1000000, 0x400, 0x1010004, 0x10000, 0x10400, 0x1000004, 0x400, 0x4, 0x1000404, 0x10404, 0x1010404, 0x10004, 0x1010000, 0x1000404, 0x1000004, 0x404, 0x10404, 0x1010400, 0x404, 0x1000400, 0x1000400, 0, 0x10004, 0x10400, 0, 0x1010004 };
            long[] spfunction2 = { -0x7fef7fe0, -0x7fff8000, 0x8000, 0x108020, 0x100000, 0x20, -0x7fefffe0, -0x7fff7fe0, -0x7fffffe0, -0x7fef7fe0, -0x7fef8000, -0x80000000, -0x7fff8000, 0x100000, 0x20, -0x7fefffe0, 0x108000, 0x100020, -0x7fff7fe0, 0, -0x80000000, 0x8000, 0x108020, -0x7ff00000, 0x100020, -0x7fffffe0, 0, 0x108000, 0x8020, -0x7fef8000, -0x7ff00000, 0x8020, 0, 0x108020, -0x7fefffe0, 0x100000, -0x7fff7fe0, -0x7ff00000, -0x7fef8000, 0x8000, -0x7ff00000, -0x7fff8000, 0x20, -0x7fef7fe0, 0x108020, 0x20, 0x8000, -0x80000000, 0x8020, -0x7fef8000, 0x100000, -0x7fffffe0, 0x100020, -0x7fff7fe0, -0x7fffffe0, 0x100020, 0x108000, 0, -0x7fff8000, 0x8020, -0x80000000, -0x7fefffe0, -0x7fef7fe0, 0x108000 };
            long[] spfunction3 = { 0x208, 0x8020200, 0, 0x8020008, 0x8000200, 0, 0x20208, 0x8000200, 0x20008, 0x8000008, 0x8000008, 0x20000, 0x8020208, 0x20008, 0x8020000, 0x208, 0x8000000, 0x8, 0x8020200, 0x200, 0x20200, 0x8020000, 0x8020008, 0x20208, 0x8000208, 0x20200, 0x20000, 0x8000208, 0x8, 0x8020208, 0x200, 0x8000000, 0x8020200, 0x8000000, 0x20008, 0x208, 0x20000, 0x8020200, 0x8000200, 0, 0x200, 0x20008, 0x8020208, 0x8000200, 0x8000008, 0x200, 0, 0x8020008, 0x8000208, 0x20000, 0x8000000, 0x8020208, 0x8, 0x20208, 0x20200, 0x8000008, 0x8020000, 0x8000208, 0x208, 0x8020000, 0x20208, 0x8, 0x8020008, 0x20200 };
            long[] spfunction4 = { 0x802001, 0x2081, 0x2081, 0x80, 0x802080, 0x800081, 0x800001, 0x2001, 0, 0x802000, 0x802000, 0x802081, 0x81, 0, 0x800080, 0x800001, 0x1, 0x2000, 0x800000, 0x802001, 0x80, 0x800000, 0x2001, 0x2080, 0x800081, 0x1, 0x2080, 0x800080, 0x2000, 0x802080, 0x802081, 0x81, 0x800080, 0x800001, 0x802000, 0x802081, 0x81, 0, 0, 0x802000, 0x2080, 0x800080, 0x800081, 0x1, 0x802001, 0x2081, 0x2081, 0x80, 0x802081, 0x81, 0x1, 0x2000, 0x800001, 0x2001, 0x802080, 0x800081, 0x2001, 0x2080, 0x800000, 0x802001, 0x80, 0x800000, 0x2000, 0x802080 };
            long[] spfunction5 = { 0x100, 0x2080100, 0x2080000, 0x42000100, 0x80000, 0x100, 0x40000000, 0x2080000, 0x40080100, 0x80000, 0x2000100, 0x40080100, 0x42000100, 0x42080000, 0x80100, 0x40000000, 0x2000000, 0x40080000, 0x40080000, 0, 0x40000100, 0x42080100, 0x42080100, 0x2000100, 0x42080000, 0x40000100, 0, 0x42000000, 0x2080100, 0x2000000, 0x42000000, 0x80100, 0x80000, 0x42000100, 0x100, 0x2000000, 0x40000000, 0x2080000, 0x42000100, 0x40080100, 0x2000100, 0x40000000, 0x42080000, 0x2080100, 0x40080100, 0x100, 0x2000000, 0x42080000, 0x42080100, 0x80100, 0x42000000, 0x42080100, 0x2080000, 0, 0x40080000, 0x42000000, 0x80100, 0x2000100, 0x40000100, 0x80000, 0, 0x40080000, 0x2080100, 0x40000100 };
            long[] spfunction6 = { 0x20000010, 0x20400000, 0x4000, 0x20404010, 0x20400000, 0x10, 0x20404010, 0x400000, 0x20004000, 0x404010, 0x400000, 0x20000010, 0x400010, 0x20004000, 0x20000000, 0x4010, 0, 0x400010, 0x20004010, 0x4000, 0x404000, 0x20004010, 0x10, 0x20400010, 0x20400010, 0, 0x404010, 0x20404000, 0x4010, 0x404000, 0x20404000, 0x20000000, 0x20004000, 0x10, 0x20400010, 0x404000, 0x20404010, 0x400000, 0x4010, 0x20000010, 0x400000, 0x20004000, 0x20000000, 0x4010, 0x20000010, 0x20404010, 0x404000, 0x20400000, 0x404010, 0x20404000, 0, 0x20400010, 0x10, 0x4000, 0x20400000, 0x404010, 0x4000, 0x400010, 0x20004010, 0, 0x20404000, 0x20000000, 0x400010, 0x20004010 };
            long[] spfunction7 = { 0x200000, 0x4200002, 0x4000802, 0, 0x800, 0x4000802, 0x200802, 0x4200800, 0x4200802, 0x200000, 0, 0x4000002, 0x2, 0x4000000, 0x4200002, 0x802, 0x4000800, 0x200802, 0x200002, 0x4000800, 0x4000002, 0x4200000, 0x4200800, 0x200002, 0x4200000, 0x800, 0x802, 0x4200802, 0x200800, 0x2, 0x4000000, 0x200800, 0x4000000, 0x200800, 0x200000, 0x4000802, 0x4000802, 0x4200002, 0x4200002, 0x2, 0x200002, 0x4000000, 0x4000800, 0x200000, 0x4200800, 0x802, 0x200802, 0x4200800, 0x802, 0x4000002, 0x4200802, 0x4200000, 0x200800, 0, 0x2, 0x4200802, 0, 0x200802, 0x4200000, 0x800, 0x4000002, 0x4000800, 0x800, 0x200002 };
            long[] spfunction8 = { 0x10001040, 0x1000, 0x40000, 0x10041040, 0x10000000, 0x10001040, 0x40, 0x10000000, 0x40040, 0x10040000, 0x10041040, 0x41000, 0x10041000, 0x41040, 0x1000, 0x40, 0x10040000, 0x10000040, 0x10001000, 0x1040, 0x41000, 0x40040, 0x10040040, 0x10041000, 0x1040, 0, 0, 0x10040040, 0x10000040, 0x10001000, 0x41040, 0x40000, 0x41040, 0x40000, 0x10041000, 0x1000, 0x40, 0x10040040, 0x1000, 0x41040, 0x10001000, 0x40, 0x10000040, 0x10040000, 0x10040040, 0x10000000, 0x40000, 0x10001040, 0, 0x10041040, 0x40040, 0x10000040, 0x10040000, 0x10001000, 0x10001040, 0, 0x10041040, 0x41000, 0x41000, 0x1040, 0x1040, 0x40040, 0x10000000, 0x10041000 };


            //create the 16 or 48 subkeys we will need
            int[] keys = des_createKeys(beinetkey);
            int m = 0;
            int i, j;
            int temp, right1, right2, left, right;
            int[] looping;
            int cbcleft = 0, cbcleft2 = 0, cbcright = 0, cbcright2 = 0;
            int endloop;
            int loopinc;
            int len = message.Length;
            int chunk = 0;
            //set up the loops for single and triple des
            var iterations = keys.Length == 32 ? 3 : 9;//single or triple des
            if (iterations == 3)
            {
                looping = encrypt ? new int[] { 0, 32, 2 } : new int[] { 30, -2, -2 };
            }
            else { looping = encrypt ? new int[] { 0, 32, 2, 62, 30, -2, 64, 96, 2 } : new int[] { 94, 62, -2, 32, 64, 2, 30, -2, -2 }; }

            if (encrypt)
            {
                message += "\0\0\0\0\0\0\0\0";//pad the message out with null bytes
            }
            //store the result here
            //List<byte> result = new List<byte>();
            //List<byte> tempresult = new List<byte>();
            string result = string.Empty;
            string tempresult = string.Empty;

            if (mode)
            {//CBC mode
                int[] tmp = { 0, 0, 0, 0, 0, 0, 0, 0 };
                int pos = 24;
                int iTmp = 0;
                while (m < iv.Length && iTmp < tmp.Length)
                {
                    if (pos < 0)
                        pos = 24;
                    tmp[iTmp++] = iv[m++] << pos;
                    pos -= 8;
                }
                cbcleft = tmp[0] | tmp[1] | tmp[2] | tmp[3];
                cbcright = tmp[4] | tmp[5] | tmp[6] | tmp[7];

                //cbcleft = (iv[m++] << 24) | (iv[m++] << 16) | (iv[m++] << 8) | iv[m++];
                //cbcright = (iv[m++] << 24) | (iv[m++] << 16) | (iv[m++] << 8) | iv[m++];
                m = 0;
            }

            //loop through each 64 bit chunk of the message
            while (m < len)
            {
                if (encrypt)
                {/*加密时按双字节操作*/
                    left = (message[m++] << 16) | message[m++];
                    right = (message[m++] << 16) | message[m++];
                }
                else
                {
                    left = (message[m++] << 24) | (message[m++] << 16) | (message[m++] << 8) | message[m++];
                    right = (message[m++] << 24) | (message[m++] << 16) | (message[m++] << 8) | message[m++];
                }
                //for Cipher Block Chaining mode,xor the message with the previous result
                if (mode)
                {
                    if (encrypt)
                    {
                        left ^= cbcleft; right ^= cbcright;
                    }
                    else
                    {
                        cbcleft2 = cbcleft; cbcright2 = cbcright; cbcleft = left; cbcright = right;
                    }
                }

                //first each 64 but chunk of the message must be permuted according to IP
                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
                temp = (RM(left, 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
                temp = (RM(right, 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

                left = ((left << 1) | RM(left, 31));
                right = ((right << 1) | RM(right, 31));

                //do this either 1 or 3 times for each chunk of the message
                for (j = 0; j < iterations; j += 3)
                {
                    endloop = looping[j + 1];
                    loopinc = looping[j + 2];
                    //now go through and perform the encryption or decryption 
                    for (i = looping[j]; i != endloop; i += loopinc)
                    {//for efficiency
                        right1 = right ^ keys[i];
                        right2 = (RM(right, 4) | (right << 28)) ^ keys[i + 1];
                        //the result is attained by passing these bytes through the S selection functions
                        temp = left;
                        left = right;
                        right = (int)(temp ^ (spfunction2[RM(right1, 24) & 0x3f] | spfunction4[RM(right1, 16) & 0x3f] | spfunction6[RM(right1, 8) & 0x3f] | spfunction8[right1 & 0x3f] | spfunction1[RM(right2, 24) & 0x3f] | spfunction3[RM(right2, 16) & 0x3f] | spfunction5[RM(right2, 8) & 0x3f] | spfunction7[right2 & 0x3f]));
                    }
                    temp = left; left = right; right = temp;//unreverse left and right
                }//for either 1 or 3 iterations

                //move then each one bit to the right
                left = (RM(left, 1) | (left << 31));
                right = (RM(right, 1) | (right << 31));

                //now perform IP-1,which is IP in the opposite direction
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(right, 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
                temp = (RM(left, 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);

                //for Cipher Block Chaining mode,xor the message with the previous result
                if (mode)
                {
                    if (encrypt)
                    {
                        cbcleft = left; cbcright = right;
                    }
                    else
                    {
                        left ^= cbcleft2; right ^= cbcright2;
                    }
                }
                //int[] arrInt;
                if (encrypt)
                {
                    //arrInt = new int[]{ RM(left, 24), (RM(left, 16) & 0xff), (RM(left, 8) & 0xff), (left & 0xff), RM(right, 24), (RM(right, 16) & 0xff), (RM(right, 8) & 0xff), (right & 0xff) };
                    tempresult += String.Concat((char)RM(left, 24),
                        (char)(RM(left, 16) & 0xff),
                        (char)(RM(left, 8) & 0xff),
                        (char)(left & 0xff),
                        (char)RM(right, 24),
                        (char)(RM(right, 16) & 0xff),
                        (char)(RM(right, 8) & 0xff),
                        (char)(right & 0xff));
                }
                else
                {
                    // 解密时，最后一个字符如果是\0，去除
                    //arrInt = new int[] { (RM(left, 16) & 0xffff), (left & 0xffff), (RM(right, 16) & 0xffff), (right & 0xffff) };
                    int tmpch = (RM(left, 16) & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (left & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (RM(right, 16) & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    tmpch = (right & 0xffff);
                    if (tmpch != 0)
                        tempresult += (char)tmpch;
                    //tempresult += String.Concat((char)(RM(left, 16) & 0xffff),
                    //    (char)(left & 0xffff),
                    //    (char)(RM(right, 16) & 0xffff),
                    //    (char)(right & 0xffff));
                }/*解密时输出双字节*/
                 //byte[] arrByte = new byte[arrInt.Length];
                 //for (int loop = 0; loop < arrInt.Length; loop++)
                 //{
                 //    tempresult.Add(byte.Parse(arrInt[loop].ToString()));
                 //    //arrByte[loop] = byte.Parse(arrInt[loop].ToString());
                 //}
                 //tempresult.Add(arrByte;// System.Text.Encoding.Unicode.GetString(arrByte);

                chunk += encrypt ? 16 : 8;
                if (chunk == 512)
                {
                    //result.AddRange(tempresult);tempresult.Clear(); 
                    result += tempresult; tempresult = string.Empty;
                    chunk = 0;
                }
            }//for every 8 characters,or 64 bits in the message

            //return the result as an array

            //result.AddRange(tempresult);
            //return result.ToArray();
            return result + tempresult;
        }//end of des

        /// <summary>
        /// this takes as input a 64 bit beinetkey(even though only 56 bits are used)
        /// as an array of 2 integers,and returns 16 48 bit keys
        /// </summary>
        /// <param name="beinetkey"></param>
        /// <returns></returns>
        static int[] des_createKeys(string beinetkey)
        {
            //declaring this locally speeds things up a bit
            int[] pc2bytes0 = { 0, 0x4, 0x20000000, 0x20000004, 0x10000, 0x10004, 0x20010000, 0x20010004, 0x200, 0x204, 0x20000200, 0x20000204, 0x10200, 0x10204, 0x20010200, 0x20010204 };
            int[] pc2bytes1 = { 0, 0x1, 0x100000, 0x100001, 0x4000000, 0x4000001, 0x4100000, 0x4100001, 0x100, 0x101, 0x100100, 0x100101, 0x4000100, 0x4000101, 0x4100100, 0x4100101 };
            int[] pc2bytes2 = { 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808, 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808 };
            int[] pc2bytes3 = { 0, 0x200000, 0x8000000, 0x8200000, 0x2000, 0x202000, 0x8002000, 0x8202000, 0x20000, 0x220000, 0x8020000, 0x8220000, 0x22000, 0x222000, 0x8022000, 0x8222000 };
            int[] pc2bytes4 = { 0, 0x40000, 0x10, 0x40010, 0, 0x40000, 0x10, 0x40010, 0x1000, 0x41000, 0x1010, 0x41010, 0x1000, 0x41000, 0x1010, 0x41010 };
            int[] pc2bytes5 = { 0, 0x400, 0x20, 0x420, 0, 0x400, 0x20, 0x420, 0x2000000, 0x2000400, 0x2000020, 0x2000420, 0x2000000, 0x2000400, 0x2000020, 0x2000420 };
            int[] pc2bytes6 = { 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002, 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002 };
            int[] pc2bytes7 = { 0, 0x10000, 0x800, 0x10800, 0x20000000, 0x20010000, 0x20000800, 0x20010800, 0x20000, 0x30000, 0x20800, 0x30800, 0x20020000, 0x20030000, 0x20020800, 0x20030800 };
            int[] pc2bytes8 = { 0, 0x40000, 0, 0x40000, 0x2, 0x40002, 0x2, 0x40002, 0x2000000, 0x2040000, 0x2000000, 0x2040000, 0x2000002, 0x2040002, 0x2000002, 0x2040002 };
            int[] pc2bytes9 = { 0, 0x10000000, 0x8, 0x10000008, 0, 0x10000000, 0x8, 0x10000008, 0x400, 0x10000400, 0x408, 0x10000408, 0x400, 0x10000400, 0x408, 0x10000408 };
            int[] pc2bytes10 = { 0, 0x20, 0, 0x20, 0x100000, 0x100020, 0x100000, 0x100020, 0x2000, 0x2020, 0x2000, 0x2020, 0x102000, 0x102020, 0x102000, 0x102020 };
            int[] pc2bytes11 = { 0, 0x1000000, 0x200, 0x1000200, 0x200000, 0x1200000, 0x200200, 0x1200200, 0x4000000, 0x5000000, 0x4000200, 0x5000200, 0x4200000, 0x5200000, 0x4200200, 0x5200200 };
            int[] pc2bytes12 = { 0, 0x1000, 0x8000000, 0x8001000, 0x80000, 0x81000, 0x8080000, 0x8081000, 0x10, 0x1010, 0x8000010, 0x8001010, 0x80010, 0x81010, 0x8080010, 0x8081010 };
            int[] pc2bytes13 = { 0, 0x4, 0x100, 0x104, 0, 0x4, 0x100, 0x104, 0x1, 0x5, 0x101, 0x105, 0x1, 0x5, 0x101, 0x105 };

            //how many iterations(1 for des,3 for triple des)
            int iterations = beinetkey.Length >= 24 ? 3 : 1;
            //stores the return keys
            int[] keys = new int[32 * iterations];
            //now define the left shifts which need to be done
            int[] shifts = { 0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0 };
            //other variables
            int left, right;
            int lefttemp;
            int righttemp;
            int m = 0, n = 0;
            int temp;

            for (int j = 0; j < iterations; j++)
            {//either 1 or 3 iterations
                int[] tmp = { 0, 0, 0, 0, 0, 0, 0, 0 };
                int pos = 24;
                int iTmp = 0;
                while (m < beinetkey.Length && iTmp < tmp.Length)
                {
                    if (pos < 0)
                        pos = 24;
                    tmp[iTmp++] = beinetkey[m++] << pos;
                    pos -= 8;
                }
                left = tmp[0] | tmp[1] | tmp[2] | tmp[3];
                right = tmp[4] | tmp[5] | tmp[6] | tmp[7];

                //left = (beinetkey[m++] << 24) | (beinetkey[m++] << 16) | (beinetkey[m++] << 8) | beinetkey[m++];
                //right = (beinetkey[m++] << 24) | (beinetkey[m++] << 16) | (beinetkey[m++] << 8) | beinetkey[m++];

                temp = (RM(left, 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
                temp = (RM(right, -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
                temp = (RM(left, 2) ^ right) & 0x33333333; right ^= temp; left ^= (temp << 2);
                temp = (RM(right, -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
                temp = (RM(right, 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
                temp = (RM(left, 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

                //the right side needs to be shifted and to get the last four bits of the left side
                temp = (left << 8) | (RM(right, 20) & 0x000000f0);
                //left needs to be put upside down
                left = (right << 24) | ((right << 8) & 0xff0000) | (RM(right, 8) & 0xff00) | (RM(right, 24) & 0xf0);
                right = temp;

                //now go through and perform these shifts on the left and right keys
                for (int i = 0; i < shifts.Length; i++)
                {
                    //shift the keys either one or two bits to the left
                    if (shifts[i] == 1)
                    {
                        left = (left << 2) | RM(left, 26); right = (right << 2) | RM(right, 26);
                    }
                    else
                    {
                        left = (left << 1) | RM(left, 27); right = (right << 1) | RM(right, 27);
                    }
                    left &= -0xf; right &= -0xf;

                    //now apply PC-2,in such a way that E is easier when encrypting or decrypting
                    //this conversion will look like PC-2 except only the last 6 bits of each byte are used
                    //rather than 48 consecutive bits and the order of lines will be according to 
                    //how the S selection functions will be applied:S2,S4,S6,S8,S1,S3,S5,S7
                    lefttemp = pc2bytes0[RM(left, 28)] | pc2bytes1[RM(left, 24) & 0xf]
                   | pc2bytes2[RM(left, 20) & 0xf] | pc2bytes3[RM(left, 16) & 0xf]
                   | pc2bytes4[RM(left, 12) & 0xf] | pc2bytes5[RM(left, 8) & 0xf]
                   | pc2bytes6[RM(left, 4) & 0xf];
                    righttemp = pc2bytes7[RM(right, 28)] | pc2bytes8[RM(right, 24) & 0xf]
                   | pc2bytes9[RM(right, 20) & 0xf] | pc2bytes10[RM(right, 16) & 0xf]
                   | pc2bytes11[RM(right, 12) & 0xf] | pc2bytes12[RM(right, 8) & 0xf]
                   | pc2bytes13[RM(right, 4) & 0xf];
                    temp = (RM(righttemp, 16) ^ lefttemp) & 0x0000ffff;
                    keys[n++] = lefttemp ^ temp; keys[n++] = righttemp ^ (temp << 16);
                }
            }//for each iterations
             //return the keys we"ve created
            return keys;
        }//end of des_createKeys

        #endregion

        #region 配合C#用的JS版本的加解密方法（需手工复制到客户端，或通过ClientScript添加到客户端）
        /// <summary>
        /// 返回JS版本的Des加解密方法字符串
        /// </summary>
        /// <returns></returns>
        public static string GetJsDesMethod()
        {
            string ret = @"

    /// 加解密主调方法
    /// beinetkey         密钥
    /// message     加密时为待加密的字符串，解密时为待解密的字符串
    /// encrypt     1：加密；0：解密
    /// mode        true：CBC mode；false：非CBC mode
    /// iv          初始化向量
    function des(beinetkey, message, encrypt, mode, iv) {
        //declaring this locally speeds things up a bit
        var spfunction1 = new Array(0x1010400, 0, 0x10000, 0x1010404, 0x1010004, 0x10404, 0x4, 0x10000, 0x400, 0x1010400, 0x1010404, 0x400, 0x1000404, 0x1010004, 0x1000000, 0x4, 0x404, 0x1000400, 0x1000400, 0x10400, 0x10400, 0x1010000, 0x1010000, 0x1000404, 0x10004, 0x1000004, 0x1000004, 0x10004, 0, 0x404, 0x10404, 0x1000000, 0x10000, 0x1010404, 0x4, 0x1010000, 0x1010400, 0x1000000, 0x1000000, 0x400, 0x1010004, 0x10000, 0x10400, 0x1000004, 0x400, 0x4, 0x1000404, 0x10404, 0x1010404, 0x10004, 0x1010000, 0x1000404, 0x1000004, 0x404, 0x10404, 0x1010400, 0x404, 0x1000400, 0x1000400, 0, 0x10004, 0x10400, 0, 0x1010004);
        var spfunction2 = new Array(-0x7fef7fe0, -0x7fff8000, 0x8000, 0x108020, 0x100000, 0x20, -0x7fefffe0, -0x7fff7fe0, -0x7fffffe0, -0x7fef7fe0, -0x7fef8000, -0x80000000, -0x7fff8000, 0x100000, 0x20, -0x7fefffe0, 0x108000, 0x100020, -0x7fff7fe0, 0, -0x80000000, 0x8000, 0x108020, -0x7ff00000, 0x100020, -0x7fffffe0, 0, 0x108000, 0x8020, -0x7fef8000, -0x7ff00000, 0x8020, 0, 0x108020, -0x7fefffe0, 0x100000, -0x7fff7fe0, -0x7ff00000, -0x7fef8000, 0x8000, -0x7ff00000, -0x7fff8000, 0x20, -0x7fef7fe0, 0x108020, 0x20, 0x8000, -0x80000000, 0x8020, -0x7fef8000, 0x100000, -0x7fffffe0, 0x100020, -0x7fff7fe0, -0x7fffffe0, 0x100020, 0x108000, 0, -0x7fff8000, 0x8020, -0x80000000, -0x7fefffe0, -0x7fef7fe0, 0x108000);
        var spfunction3 = new Array(0x208, 0x8020200, 0, 0x8020008, 0x8000200, 0, 0x20208, 0x8000200, 0x20008, 0x8000008, 0x8000008, 0x20000, 0x8020208, 0x20008, 0x8020000, 0x208, 0x8000000, 0x8, 0x8020200, 0x200, 0x20200, 0x8020000, 0x8020008, 0x20208, 0x8000208, 0x20200, 0x20000, 0x8000208, 0x8, 0x8020208, 0x200, 0x8000000, 0x8020200, 0x8000000, 0x20008, 0x208, 0x20000, 0x8020200, 0x8000200, 0, 0x200, 0x20008, 0x8020208, 0x8000200, 0x8000008, 0x200, 0, 0x8020008, 0x8000208, 0x20000, 0x8000000, 0x8020208, 0x8, 0x20208, 0x20200, 0x8000008, 0x8020000, 0x8000208, 0x208, 0x8020000, 0x20208, 0x8, 0x8020008, 0x20200);
        var spfunction4 = new Array(0x802001, 0x2081, 0x2081, 0x80, 0x802080, 0x800081, 0x800001, 0x2001, 0, 0x802000, 0x802000, 0x802081, 0x81, 0, 0x800080, 0x800001, 0x1, 0x2000, 0x800000, 0x802001, 0x80, 0x800000, 0x2001, 0x2080, 0x800081, 0x1, 0x2080, 0x800080, 0x2000, 0x802080, 0x802081, 0x81, 0x800080, 0x800001, 0x802000, 0x802081, 0x81, 0, 0, 0x802000, 0x2080, 0x800080, 0x800081, 0x1, 0x802001, 0x2081, 0x2081, 0x80, 0x802081, 0x81, 0x1, 0x2000, 0x800001, 0x2001, 0x802080, 0x800081, 0x2001, 0x2080, 0x800000, 0x802001, 0x80, 0x800000, 0x2000, 0x802080);
        var spfunction5 = new Array(0x100, 0x2080100, 0x2080000, 0x42000100, 0x80000, 0x100, 0x40000000, 0x2080000, 0x40080100, 0x80000, 0x2000100, 0x40080100, 0x42000100, 0x42080000, 0x80100, 0x40000000, 0x2000000, 0x40080000, 0x40080000, 0, 0x40000100, 0x42080100, 0x42080100, 0x2000100, 0x42080000, 0x40000100, 0, 0x42000000, 0x2080100, 0x2000000, 0x42000000, 0x80100, 0x80000, 0x42000100, 0x100, 0x2000000, 0x40000000, 0x2080000, 0x42000100, 0x40080100, 0x2000100, 0x40000000, 0x42080000, 0x2080100, 0x40080100, 0x100, 0x2000000, 0x42080000, 0x42080100, 0x80100, 0x42000000, 0x42080100, 0x2080000, 0, 0x40080000, 0x42000000, 0x80100, 0x2000100, 0x40000100, 0x80000, 0, 0x40080000, 0x2080100, 0x40000100);
        var spfunction6 = new Array(0x20000010, 0x20400000, 0x4000, 0x20404010, 0x20400000, 0x10, 0x20404010, 0x400000, 0x20004000, 0x404010, 0x400000, 0x20000010, 0x400010, 0x20004000, 0x20000000, 0x4010, 0, 0x400010, 0x20004010, 0x4000, 0x404000, 0x20004010, 0x10, 0x20400010, 0x20400010, 0, 0x404010, 0x20404000, 0x4010, 0x404000, 0x20404000, 0x20000000, 0x20004000, 0x10, 0x20400010, 0x404000, 0x20404010, 0x400000, 0x4010, 0x20000010, 0x400000, 0x20004000, 0x20000000, 0x4010, 0x20000010, 0x20404010, 0x404000, 0x20400000, 0x404010, 0x20404000, 0, 0x20400010, 0x10, 0x4000, 0x20400000, 0x404010, 0x4000, 0x400010, 0x20004010, 0, 0x20404000, 0x20000000, 0x400010, 0x20004010);
        var spfunction7 = new Array(0x200000, 0x4200002, 0x4000802, 0, 0x800, 0x4000802, 0x200802, 0x4200800, 0x4200802, 0x200000, 0, 0x4000002, 0x2, 0x4000000, 0x4200002, 0x802, 0x4000800, 0x200802, 0x200002, 0x4000800, 0x4000002, 0x4200000, 0x4200800, 0x200002, 0x4200000, 0x800, 0x802, 0x4200802, 0x200800, 0x2, 0x4000000, 0x200800, 0x4000000, 0x200800, 0x200000, 0x4000802, 0x4000802, 0x4200002, 0x4200002, 0x2, 0x200002, 0x4000000, 0x4000800, 0x200000, 0x4200800, 0x802, 0x200802, 0x4200800, 0x802, 0x4000002, 0x4200802, 0x4200000, 0x200800, 0, 0x2, 0x4200802, 0, 0x200802, 0x4200000, 0x800, 0x4000002, 0x4000800, 0x800, 0x200002);
        var spfunction8 = new Array(0x10001040, 0x1000, 0x40000, 0x10041040, 0x10000000, 0x10001040, 0x40, 0x10000000, 0x40040, 0x10040000, 0x10041040, 0x41000, 0x10041000, 0x41040, 0x1000, 0x40, 0x10040000, 0x10000040, 0x10001000, 0x1040, 0x41000, 0x40040, 0x10040040, 0x10041000, 0x1040, 0, 0, 0x10040040, 0x10000040, 0x10001000, 0x41040, 0x40000, 0x41040, 0x40000, 0x10041000, 0x1000, 0x40, 0x10040040, 0x1000, 0x41040, 0x10001000, 0x40, 0x10000040, 0x10040000, 0x10040040, 0x10000000, 0x40000, 0x10001040, 0, 0x10041040, 0x40040, 0x10000040, 0x10040000, 0x10001000, 0x10001040, 0, 0x10041040, 0x41000, 0x41000, 0x1040, 0x1040, 0x40040, 0x10000000, 0x10041000);

        //create the 16 or 48 subkeys we will need
        var keys = des_createKeys(beinetkey);
        var m = 0, i, j, temp, temp2, right1, right2, left, right, looping;
        var cbcleft, cbcleft2, cbcright, cbcright2
        var endloop, loopinc;
        var len = message.length;
        var chunk = 0;
        //set up the loops for single and triple des
        var iterations = keys.length == 32 ? 3 : 9; //single or triple des
        if (iterations == 3) { looping = encrypt ? new Array(0, 32, 2) : new Array(30, -2, -2); }
        else { looping = encrypt ? new Array(0, 32, 2, 62, 30, -2, 64, 96, 2) : new Array(94, 62, -2, 32, 64, 2, 30, -2, -2); }

        message += '\0\0\0\0\0\0\0\0'; //pad the message out with null bytes
        //store the result here
        result = '';
        tempresult = '';

        if (mode == 1) {//CBC mode
            cbcleft = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
            cbcright = (iv.charCodeAt(m++) << 24) | (iv.charCodeAt(m++) << 16) | (iv.charCodeAt(m++) << 8) | iv.charCodeAt(m++);
            m = 0;
        }

        //loop through each 64 bit chunk of the message
        while (m < len) {
            if (encrypt) {//加密时按双字节操作
                left = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
                right = (message.charCodeAt(m++) << 16) | message.charCodeAt(m++);
            } else {
                left = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
                right = (message.charCodeAt(m++) << 24) | (message.charCodeAt(m++) << 16) | (message.charCodeAt(m++) << 8) | message.charCodeAt(m++);
            }
            //for Cipher Block Chaining mode,xor the message with the previous result
            if (mode == 1) { if (encrypt) { left ^= cbcleft; right ^= cbcright; } else { cbcleft2 = cbcleft; cbcright2 = cbcright; cbcleft = left; cbcright = right; } }

            //first each 64 but chunk of the message must be permuted according to IP
            temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
            temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
            temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
            temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
            temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

            left = ((left << 1) | (left >>> 31));
            right = ((right << 1) | (right >>> 31));

            //do this either 1 or 3 times for each chunk of the message
            for (j = 0; j < iterations; j += 3) {
                endloop = looping[j + 1];
                loopinc = looping[j + 2];
                //now go through and perform the encryption or decryption 
                for (i = looping[j]; i != endloop; i += loopinc) {//for efficiency
                    right1 = right ^ keys[i];
                    right2 = ((right >>> 4) | (right << 28)) ^ keys[i + 1];
                    //the result is attained by passing these bytes through the S selection functions
                    temp = left;
                    left = right;
                    right = temp ^ (spfunction2[(right1 >>> 24) & 0x3f] | spfunction4[(right1 >>> 16) & 0x3f] | spfunction6[(right1 >>> 8) & 0x3f] | spfunction8[right1 & 0x3f] | spfunction1[(right2 >>> 24) & 0x3f] | spfunction3[(right2 >>> 16) & 0x3f] | spfunction5[(right2 >>> 8) & 0x3f] | spfunction7[right2 & 0x3f]);
                }
                temp = left; left = right; right = temp; //unreverse left and right
            } //for either 1 or 3 iterations

            //move then each one bit to the right
            left = ((left >>> 1) | (left << 31));
            right = ((right >>> 1) | (right << 31));

            //now perform IP-1,which is IP in the opposite direction
            temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
            temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
            temp = ((right >>> 2) ^ left) & 0x33333333; left ^= temp; right ^= (temp << 2);
            temp = ((left >>> 16) ^ right) & 0x0000ffff; right ^= temp; left ^= (temp << 16);
            temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);

            //for Cipher Block Chaining mode,xor the message with the previous result
            if (mode == 1) { if (encrypt) { cbcleft = left; cbcright = right; } else { left ^= cbcleft2; right ^= cbcright2; } }
            if (encrypt) {
                tempresult += String.fromCharCode((left >>> 24), ((left >>> 16) & 0xff), ((left >>> 8) & 0xff), (left & 0xff), (right >>> 24), ((right >>> 16) & 0xff), ((right >>> 8) & 0xff), (right & 0xff));
            }
            else { 
                tempresult += String.fromCharCode(((left >>> 16) & 0xffff), (left & 0xffff), ((right >>> 16) & 0xffff), (right & 0xffff)); 
            } //解密时输出双字节
            encrypt ? chunk += 16 : chunk += 8;
            if (chunk == 512) { result += tempresult; tempresult = ''; chunk = 0; }
        } //for every 8 characters,or 64 bits in the message

        //return the result as an array
        return result + tempresult;
    } //end of des

//des_createKeys
//this takes as input a 64 bit beinetkey(even though only 56 bits are used)
//as an array of 2 integers,and returns 16 48 bit keys
    function des_createKeys(beinetkey) {
        //declaring this locally speeds things up a bit
        pc2bytes0 = new Array(0, 0x4, 0x20000000, 0x20000004, 0x10000, 0x10004, 0x20010000, 0x20010004, 0x200, 0x204, 0x20000200, 0x20000204, 0x10200, 0x10204, 0x20010200, 0x20010204);
        pc2bytes1 = new Array(0, 0x1, 0x100000, 0x100001, 0x4000000, 0x4000001, 0x4100000, 0x4100001, 0x100, 0x101, 0x100100, 0x100101, 0x4000100, 0x4000101, 0x4100100, 0x4100101);
        pc2bytes2 = new Array(0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808, 0, 0x8, 0x800, 0x808, 0x1000000, 0x1000008, 0x1000800, 0x1000808);
        pc2bytes3 = new Array(0, 0x200000, 0x8000000, 0x8200000, 0x2000, 0x202000, 0x8002000, 0x8202000, 0x20000, 0x220000, 0x8020000, 0x8220000, 0x22000, 0x222000, 0x8022000, 0x8222000);
        pc2bytes4 = new Array(0, 0x40000, 0x10, 0x40010, 0, 0x40000, 0x10, 0x40010, 0x1000, 0x41000, 0x1010, 0x41010, 0x1000, 0x41000, 0x1010, 0x41010);
        pc2bytes5 = new Array(0, 0x400, 0x20, 0x420, 0, 0x400, 0x20, 0x420, 0x2000000, 0x2000400, 0x2000020, 0x2000420, 0x2000000, 0x2000400, 0x2000020, 0x2000420);
        pc2bytes6 = new Array(0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002, 0, 0x10000000, 0x80000, 0x10080000, 0x2, 0x10000002, 0x80002, 0x10080002);
        pc2bytes7 = new Array(0, 0x10000, 0x800, 0x10800, 0x20000000, 0x20010000, 0x20000800, 0x20010800, 0x20000, 0x30000, 0x20800, 0x30800, 0x20020000, 0x20030000, 0x20020800, 0x20030800);
        pc2bytes8 = new Array(0, 0x40000, 0, 0x40000, 0x2, 0x40002, 0x2, 0x40002, 0x2000000, 0x2040000, 0x2000000, 0x2040000, 0x2000002, 0x2040002, 0x2000002, 0x2040002);
        pc2bytes9 = new Array(0, 0x10000000, 0x8, 0x10000008, 0, 0x10000000, 0x8, 0x10000008, 0x400, 0x10000400, 0x408, 0x10000408, 0x400, 0x10000400, 0x408, 0x10000408);
        pc2bytes10 = new Array(0, 0x20, 0, 0x20, 0x100000, 0x100020, 0x100000, 0x100020, 0x2000, 0x2020, 0x2000, 0x2020, 0x102000, 0x102020, 0x102000, 0x102020);
        pc2bytes11 = new Array(0, 0x1000000, 0x200, 0x1000200, 0x200000, 0x1200000, 0x200200, 0x1200200, 0x4000000, 0x5000000, 0x4000200, 0x5000200, 0x4200000, 0x5200000, 0x4200200, 0x5200200);
        pc2bytes12 = new Array(0, 0x1000, 0x8000000, 0x8001000, 0x80000, 0x81000, 0x8080000, 0x8081000, 0x10, 0x1010, 0x8000010, 0x8001010, 0x80010, 0x81010, 0x8080010, 0x8081010);
        pc2bytes13 = new Array(0, 0x4, 0x100, 0x104, 0, 0x4, 0x100, 0x104, 0x1, 0x5, 0x101, 0x105, 0x1, 0x5, 0x101, 0x105);

        //how many iterations(1 for des,3 for triple des)
        var iterations = beinetkey.length >= 24 ? 3 : 1;
        //stores the return keys
        var keys = new Array(32 * iterations);
        //now define the left shifts which need to be done
        var shifts = new Array(0, 0, 1, 1, 1, 1, 1, 1, 0, 1, 1, 1, 1, 1, 1, 0);
        //other variables
        var lefttemp, righttemp, m = 0, n = 0, temp;

        for (var j = 0; j < iterations; j++) {//either 1 or 3 iterations
            left = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);
            right = (beinetkey.charCodeAt(m++) << 24) | (beinetkey.charCodeAt(m++) << 16) | (beinetkey.charCodeAt(m++) << 8) | beinetkey.charCodeAt(m++);

            temp = ((left >>> 4) ^ right) & 0x0f0f0f0f; right ^= temp; left ^= (temp << 4);
            temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
            temp = ((left >>> 2) ^ right) & 0x33333333; right ^= temp; left ^= (temp << 2);
            temp = ((right >>> -16) ^ left) & 0x0000ffff; left ^= temp; right ^= (temp << -16);
            temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);
            temp = ((right >>> 8) ^ left) & 0x00ff00ff; left ^= temp; right ^= (temp << 8);
            temp = ((left >>> 1) ^ right) & 0x55555555; right ^= temp; left ^= (temp << 1);

            //the right side needs to be shifted and to get the last four bits of the left side
            temp = (left << 8) | ((right >>> 20) & 0x000000f0);
            //left needs to be put upside down
            left = (right << 24) | ((right << 8) & 0xff0000) | ((right >>> 8) & 0xff00) | ((right >>> 24) & 0xf0);
            right = temp;

            //now go through and perform these shifts on the left and right keys
            for (i = 0; i < shifts.length; i++) {
                //shift the keys either one or two bits to the left
                if (shifts[i]) { left = (left << 2) | (left >>> 26); right = (right << 2) | (right >>> 26); }
                else { left = (left << 1) | (left >>> 27); right = (right << 1) | (right >>> 27); }
                left &= -0xf; right &= -0xf;

                //now apply PC-2,in such a way that E is easier when encrypting or decrypting
                //this conversion will look like PC-2 except only the last 6 bits of each byte are used
                //rather than 48 consecutive bits and the order of lines will be according to 
                //how the S selection functions will be applied:S2,S4,S6,S8,S1,S3,S5,S7
                lefttemp = pc2bytes0[left >>> 28] | pc2bytes1[(left >>> 24) & 0xf]
| pc2bytes2[(left >>> 20) & 0xf] | pc2bytes3[(left >>> 16) & 0xf]
| pc2bytes4[(left >>> 12) & 0xf] | pc2bytes5[(left >>> 8) & 0xf]
| pc2bytes6[(left >>> 4) & 0xf];
                righttemp = pc2bytes7[right >>> 28] | pc2bytes8[(right >>> 24) & 0xf]
| pc2bytes9[(right >>> 20) & 0xf] | pc2bytes10[(right >>> 16) & 0xf]
| pc2bytes11[(right >>> 12) & 0xf] | pc2bytes12[(right >>> 8) & 0xf]
| pc2bytes13[(right >>> 4) & 0xf];
                temp = ((righttemp >>> 16) ^ lefttemp) & 0x0000ffff;
                keys[n++] = lefttemp ^ temp; keys[n++] = righttemp ^ (temp << 16);
            }
        } //for each iterations
        //return the keys we've created
        return keys;
    } //end of des_createKeys


///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////

// 把字符串转换为16进制字符串
// 如：a变成61（即10进制的97）；abc变成616263
function stringToHex(s) {
 var r='';
 var hexes=new Array('0','1','2','3','4','5','6','7','8','9','a','b','c','d','e','f');
 for(var i=0;i<(s.length);i++){r+=hexes[s.charCodeAt(i)>>4]+hexes[s.charCodeAt(i)&0xf];}
 return r;
}
// 16进制字符串转换为字符串
// 如：61（即10进制的97）变成a；616263变成abc
function HexTostring(s){
 var r='';
 for(var i=0;i<s.length;i+=2){
 var sxx=parseInt(s.substring(i,i+2),16);
 r+=String.fromCharCode(sxx);}
 return r;
}

/// 加密测试函数
/// s     待加密的字符串
/// k     密钥
function encMe(s, k){
    return stringToHex(des(k,s,1,0));
}

/// 解密测试函数
/// s     待解密的字符串
/// k     密钥
function uncMe(s, k){
    return des(k,HexTostring(s),0,0);
}
";
            return ret;
        }
        #endregion

        #region 以前用的DES加解密函数，跟上面的不能通用
        /// <summary>
        /// 使用DES加密指定字符串
        /// </summary>
        /// <param name="encryptStr">待加密的字符串</param>
        /// <param name="beinetkey">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>加密后的字符串</returns>
        public static string DESEncrypt(string encryptStr, string beinetkey, string IV)
        {
            //将beinetkey和IV处理成8个字符
            beinetkey += "12345678";
            IV += "12345678";
            beinetkey = beinetkey.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            sa = new DESCryptoServiceProvider();
            sa.Key = Encoding.UTF8.GetBytes(beinetkey);
            sa.IV = Encoding.UTF8.GetBytes(IV);
            ict = sa.CreateEncryptor();

            byt = Encoding.UTF8.GetBytes(encryptStr);

            ms = new MemoryStream();
            cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
            cs.Write(byt, 0, byt.Length);
            cs.FlushFinalBlock();

            cs.Close();

            //加上一些干扰字符
            string retVal = Convert.ToBase64String(ms.ToArray());
            System.Random ra = new Random();

            for (int i = 0; i < 8; i++)
            {
                int radNum = ra.Next(36);
                char radChr = Convert.ToChar(radNum + 65);//生成一个随机字符

                retVal = retVal.Substring(0, 2 * i + 1) + radChr.ToString() + retVal.Substring(2 * i + 1);
            }

            return retVal;
        }

        /// <summary>
        /// 使用DES加密指定字符串
        /// </summary>
        /// <param name="encryptStr">待加密的字符串</param>
        /// <param name="beinetkey">密钥和初始化向量(最大长度8)</param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptStr, string beinetkey)
        {
            if (beinetkey == null || beinetkey == "")
            {
                beinetkey = "beinetcn";
            }
            return DESEncrypt(encryptStr, beinetkey, beinetkey);
        }

        /// <summary>
        /// 使用DES加密指定字符串
        /// </summary>
        /// <param name="encryptStr"></param>
        /// <returns></returns>
        public static string DESEncrypt(string encryptStr)
        {
            return DESEncrypt(encryptStr, "beinetcn", "beinetcn");
        }

        /// <summary>
        /// 使用DES解密指定字符串
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="beinetkey">密钥(最大长度8)</param>
        /// <param name="IV">初始化向量(最大长度8)</param>
        /// <returns>解密后的字符串</returns>
        public static string DESDecrypt(string encryptedValue, string beinetkey, string IV)
        {
            //去掉干扰字符
            string tmp = encryptedValue;
            if (tmp.Length < 16)
            {
                return "";
            }

            for (int i = 0; i < 8; i++)
            {
                tmp = tmp.Substring(0, i + 1) + tmp.Substring(i + 2);
            }
            encryptedValue = tmp;

            //将beinetkey和IV处理成8个字符
            beinetkey += "12345678";
            IV += "12345678";
            beinetkey = beinetkey.Substring(0, 8);
            IV = IV.Substring(0, 8);

            SymmetricAlgorithm sa;
            ICryptoTransform ict;
            MemoryStream ms;
            CryptoStream cs;
            byte[] byt;

            try
            {
                sa = new DESCryptoServiceProvider();
                sa.Key = Encoding.UTF8.GetBytes(beinetkey);
                sa.IV = Encoding.UTF8.GetBytes(IV);
                ict = sa.CreateDecryptor();

                byt = Convert.FromBase64String(encryptedValue);

                ms = new MemoryStream();
                cs = new CryptoStream(ms, ict, CryptoStreamMode.Write);
                cs.Write(byt, 0, byt.Length);
                cs.FlushFinalBlock();

                cs.Close();

                return Encoding.UTF8.GetString(ms.ToArray());
            }
            catch (System.Exception)
            {
                return "";
            }

        }

        /// <summary>
        /// 使用DES解密指定字符串
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <param name="beinetkey">密钥和初始化向量(最大长度8)</param>
        /// <returns></returns>
        public static string DESDecrypt(string encryptedValue, string beinetkey)
        {
            if (beinetkey == null || beinetkey == "")
            {
                beinetkey = "beinetcn";
            }
            return DESDecrypt(encryptedValue, beinetkey, beinetkey);
        }

        /// <summary>
        /// 使用DES解密指定字符串
        /// </summary>
        /// <param name="encryptedValue">待解密的字符串</param>
        /// <returns></returns>
        public static string DESDecrypt(string encryptedValue)
        {
            return DESDecrypt(encryptedValue, "beinetcn", "beinetcn");
        }
        #endregion

        #endregion
    }
}
