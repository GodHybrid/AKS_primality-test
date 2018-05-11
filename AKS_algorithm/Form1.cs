using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Windows.Forms;
using System.Threading;

namespace AKS_algorithm
{
    public partial class Form1 : Form
    {
        static BigInteger[] c = new BigInteger[10000];
        private Thread t;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            SwitchEnabledStates();
            t.Abort();
        }

        private void SwitchEnabledStates()
        {
            button1.Enabled = !button1.Enabled;
            button3.Enabled = !button3.Enabled;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ushort TryRes;
            try
            {
                UInt16.TryParse(textBox3.Text, out TryRes);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            if (textBox3.Text == String.Empty || TryRes < 3)
            {
                return;
            }
            else
            {
                SwitchEnabledStates();
                t = new Thread(StartAKS);
                t.Start();
                AKS(TryRes);
            }
        }

        private void StartAKS()
        {
            ushort N = UInt16.Parse(textBox3.Text);
            AKS(N);
            t.Abort();
        }

        void AKS(ushort n)
        {
            for (int i = 0; i < n; i++)
            {
                coef(i);
                textBox2.Text += "(x-1)^" + i + " = ";
                show(i);
                textBox2.Text += "\r\n";
            }
            textBox1.Text += "Primes:\r\n";
            for (int i = 1; i <= n; i++)
                if (is_prime(i))
                    textBox1.Text += i + " ";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void radioButton1_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = false;
            numericUpDown1.Enabled = false;
        }

        private void radioButton2_Click(object sender, EventArgs e)
        {
            checkBox1.Enabled = true;
            numericUpDown1.Enabled = true;
        }

        static void coef(int n)
        {
            int i, j;

            for (c[i = 0] = 1L; i < n; c[0] = -c[0], i++)
                for (c[1 + (j = i)] = 1L; j > 0; j--)
                    c[j] = c[j - 1] - c[j];
        }

        static bool is_prime(int n)
        {
            int i;

            coef(n);
            c[0] += 1;
            c[i = n] -= 1;

            while (i-- != 0 && (c[i] % n) == 0) ;

            return i < 0;
        }

        void show(int n)
        {
            do
            {
                textBox2.Text += "+" + c[n] + "x^" + n;
            } while (n-- != 0);
        }
    }
}
