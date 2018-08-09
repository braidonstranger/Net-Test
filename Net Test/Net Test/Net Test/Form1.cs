using System;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.Security.Cryptography;

namespace Net_Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (textBox1.Text == "")
                {
                    MessageBox.Show("Password cannot be blank");
                    return;
                }

                WebClient wc = new WebClient();

                if (SHA256Converter((wc.DownloadString("https://staela.net/files/himom.txt")).Substring(0, (wc.DownloadString("https://staela.net/files/himom.txt")).Length - 1)) == SHA256Converter(MD5Converter(textBox1.Text))) {
                    Thread t = new Thread(new ThreadStart(() => {
                        Application.Run(new Form2());
                    }));
                    t.Start();
                    this.Close();
                } else {
                    MessageBox.Show("Password Incorrect");
                    textBox1.Text = "";
                }

            } catch (Exception ex) {
                MessageBox.Show("Unable to login at this moment.  Please try again later.");
            }
        }

        protected String MD5Converter (String text)
        {
            MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider();
            md5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = md5.Hash;
            StringBuilder str = new StringBuilder();
            for (int i =0; i < result.Length; i ++)
            {
                str.Append(result[i].ToString("x2"));
            }

            return str.ToString();
        }

        protected String SHA256Converter (String text)
        {
            SHA256CryptoServiceProvider sha = new SHA256CryptoServiceProvider();
            sha.ComputeHash(ASCIIEncoding.ASCII.GetBytes(text));
            byte[] result = sha.Hash;
            StringBuilder str = new StringBuilder();
            for (int i =0; i < result.Length; i ++)
            {
                str.Append(result[i].ToString("x2"));
            }

            return str.ToString();
        }
    }
}
