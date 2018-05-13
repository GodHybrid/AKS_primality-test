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
        String Equation = String.Empty;
        private Thread t;
        static Thread MainThread = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MainThread = Thread.CurrentThread;
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
            textBox1.Text += "Primes:\r\n";
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
                textBox1.Text = "Primes:\r\n";
                t = new Thread(StartAKS);
                t.Start();
            }
        }

        private void StartAKS()
        {
            ushort N = UInt16.Parse(textBox3.Text);
            AKS(N);
        }

        void AKS(ushort n)
        {
            for (int i = 1; i <= n; i++)
            {
                String tmp = String.Empty;
                coef(i);
                Equation += "(x-1)^" + i + " =";
                show(i);
                tmp = Equation;
                Equation = String.Empty;
                textBox2.Invoke( (Action)(() => { textBox2.Text += tmp; }) );
                //Task.Run( () => { textBox2.Text += tmp; } );
                if (i > 1 && is_prime(i))
                {
                    textBox1.Invoke( (Action)(() => { textBox1.Text += i + " "; }) );
                }
            }
            t.Abort();
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
            if (n >= 1)
            {
                int i;

                coef(n);
                c[0] += 1;
                c[i = n] -= 1;

                while (i-- != 0 && (c[i] % n) == 0) ;

                return i < 0;
            }
            else return false;
        }

        void show(int n)
        {
            do
            {
                if (c[n] < 0) Equation += " - " + c[n] + "x^" + n;
                else Equation += " + " + c[n] + "x^" + n;
            } while (n-- != 0);
            Equation += "\r\n";
        }
    }
}
