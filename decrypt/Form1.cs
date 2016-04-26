using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {    
                string text = Application.StartupPath + "\\set.ini";
                FileInfo fileInfo = new FileInfo(text);
                
                StreamReader streamReader = new StreamReader(text);
                string text2=streamReader.ReadLine();;
                
                text2 = text2.Substring(8);
                int num = text2.LastIndexOf("Password=") + 8;
                string text3 = text2.Substring(num + 1, text2.Length - num - 1);
                text3 = Form1.DecryptoPassword(text3);
                string str = text2.Substring(0, num + 1 );
                textBox1.Text= text3;
        }

            public static string DecryptoPassword(string EncryptoPassword)
            {
                string value = "qingfeng";
                EncryptoPassword = DecryptEncrypt.MyDecryptEncrypt.Decrypto(EncryptoPassword);
                EncryptoPassword = EncryptoPassword.Substring(0, EncryptoPassword.IndexOf(value));
                EncryptoPassword = DecryptEncrypt.MyDecryptEncrypt.Decrypto(EncryptoPassword);
                return EncryptoPassword;
            }   
    }
}
