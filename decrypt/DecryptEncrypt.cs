using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace WindowsFormsApplication1
{
    class DecryptEncrypt
    {
        private SymmetricAlgorithm mobjCryptoService;

        private string Key;

        public static DecryptEncrypt MyDecryptEncrypt
        {
            get
            {
                return new DecryptEncrypt();
            }
        }

        internal DecryptEncrypt()
        {
            this.mobjCryptoService = new RijndaelManaged();
            this.Key = "rrp(%&h70x89H$jgsfgfsI0456Ftma81&fvHrr&&76*h%(12lJ$lhj!y6&(*jkPer44a";
        }

        private byte[] GetLegalKey()
        {
            string text = this.Key;
            this.mobjCryptoService.GenerateKey();
            byte[] key = this.mobjCryptoService.Key;
            int num = key.Length;
            if (text.Length > num)
            {
                text = text.Substring(0, num);
            }
            else if (text.Length < num)
            {
                text = text.PadRight(num, ' ');
            }
            return Encoding.ASCII.GetBytes(text);
        }

        private byte[] GetLegalIV()
        {
            string text = "@afetj*Ghg7!rNIfsgr95GUqd9gsrb#GG7HBh(urjj6HJ($jhWk7&!hjjri%$hjk";
            this.mobjCryptoService.GenerateIV();
            byte[] iV = this.mobjCryptoService.IV;
            int num = iV.Length;
            if (text.Length > num)
            {
                text = text.Substring(0, num);
            }
            else if (text.Length < num)
            {
                text = text.PadRight(num, ' ');
            }
            return Encoding.ASCII.GetBytes(text);
        }

        public string Encrypto(string Source)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(Source);
            MemoryStream memoryStream = new MemoryStream();
            this.mobjCryptoService.Key = this.GetLegalKey();
            this.mobjCryptoService.IV = this.GetLegalIV();
            ICryptoTransform transform = this.mobjCryptoService.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
            cryptoStream.Write(bytes, 0, bytes.Length);
            cryptoStream.FlushFinalBlock();
            memoryStream.Close();
            byte[] inArray = memoryStream.ToArray();
            return Convert.ToBase64String(inArray);
        }

        public string Decrypto(string Source)
        {
            byte[] array = Convert.FromBase64String(Source);
            MemoryStream stream = new MemoryStream(array, 0, array.Length);
            this.mobjCryptoService.Key = this.GetLegalKey();
            this.mobjCryptoService.IV = this.GetLegalIV();
            ICryptoTransform transform = this.mobjCryptoService.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(stream2);
            return streamReader.ReadToEnd();
        }

        public string Decrypto2(string Source)
        {
            byte[] array = Convert.FromBase64String(Source);
            MemoryStream stream = new MemoryStream(array, 0, array.Length);
            this.mobjCryptoService.Key = this.GetLegalKey();
            this.mobjCryptoService.IV = this.GetLegalIV();
            ICryptoTransform transform = this.mobjCryptoService.CreateDecryptor();
            CryptoStream stream2 = new CryptoStream(stream, transform, CryptoStreamMode.Read);
            StreamReader streamReader = new StreamReader(stream2);
            string text = streamReader.ReadToEnd();
            int num = (int)Math.Ceiling(Convert.ToDouble(text.Length) / 3.0);
            int num2 = text.Length - num * 2;
            string text2 = "";
            if (text.Length > num)
            {
                if (num2 > 0)
                {
                    text = text.Substring(0, num2) + text2.PadLeft(text.Substring(num2, num).Length, '*') + text.Substring(num2 + num);
                }
                else
                {
                    text = text2.PadLeft(text.Length - num, '*') + text.Substring(num2 + num - 1);
                }
            }
            return text;
        }

        public string Decrypto3(string Source, bool authorize)
        {
            try
            {
                return authorize ? this.Decrypto(Source) : this.Decrypto2(Source);
            }
            catch
            {
            }
            return Source;
        }

        public string StarEncrypto(string s)
        {
            int num = (int)Math.Ceiling(Convert.ToDouble(s.Length) / 3.0);
            int num2 = s.Length - num * 2;
            string text = "";
            if (s.Length > num)
            {
                if (num2 > 0)
                {
                    s = s.Substring(0, num2) + text.PadLeft(s.Substring(num2, num).Length, '*') + s.Substring(num2 + num);
                }
                else
                {
                    s = text.PadLeft(s.Length - num, '*') + s.Substring(num2 + num - 1);
                }
            }
            return s;
        }

        public string StarEncrypto(string s, bool authorize)
        {
            if (!authorize)
            {
                return this.StarEncrypto(s);
            }
            return s;
        }
    }
}
