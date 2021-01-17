using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
//1.To use the RSA algorithm in C#, we need to add the following namespace:
using System.Security.Cryptography;

namespace security_project
{
    public partial class Form1 : Form
    {
        //2.Now make a function for Encryption.
        static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] encryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
                }
                return encryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        //3.Now make a function for Decryption

        static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
        {
            try
            {
                byte[] decryptedData;
                using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
                {
                    RSA.ImportParameters(RSAKey);
                    decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
                }
                return decryptedData;
            }
            catch (CryptographicException e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }

        //4. Now make some variables into the class that are:
        UnicodeEncoding ByteConverter = new UnicodeEncoding();
        RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
        byte[] plaintext;
        byte[] encryptedtext;

        public Form1()
        {
            InitializeComponent();
        }
        //5. Now handle the Click Event for the Encrypt Button with the following code:
        private void button1_Click(object sender, EventArgs e)
        {
            //if (File.Exists("F:\\MytText.txt"))
            //{
            //    richTextBox1.Lines = File.ReadAllLines("F:\\MytText.txt");
            //}

            //if (File.Exists("F:\\MytText.txt"))
            //{
            //    richTextBox1.Text = File.ReadAllText("F:\\MytText.txt");
            //}

            //plaintext = ByteConverter.GetBytes(txtplain.Text);


            plaintext = ByteConverter.GetBytes(txtplain.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            txtencrypt.Text = ByteConverter.GetString(encryptedtext);


        }

        //6. Now handle the Click Event for the Decrypt Button with the following code:
        private void button2_Click(object sender, EventArgs e)
        {
            byte[] decryptedtex = Decryption(encryptedtext,
            RSA.ExportParameters(true), false);
            txtdecrypt.Text = ByteConverter.GetString(decryptedtex);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Stream mystream;

            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                if((mystream = openFileDialog.OpenFile()) != null)
                {
                    string strfilename = openFileDialog.FileName;
                    string filetext = File.ReadAllText(strfilename);
                    txtplain.Text = filetext;
                }
            }
            //if (File.Exists("F:\\MytText.txt"))
            //{
            //    txtplain.Text = File.ReadAllText("F:\\MytText.txt");
            //}
        }

        private void button4_Click(object sender, EventArgs e)
        {
            plaintext = ByteConverter.GetBytes(txtplain.Text);
            encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);
            File.WriteAllText("F:\\Encrypted.txt" ,ByteConverter.GetString(encryptedtext));
        }
    }
}
