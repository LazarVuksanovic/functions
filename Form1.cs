using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace Funkcije
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        Funkcija f, f2, f3;
        KoordinatniSistem k;
        Graphics g;
        Pen p, p1, p2, p3;
        bool rez = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            //Дефинисање
            g = pictureBox1.CreateGraphics();
            p = new Pen(Color.Black);
            p1 = new Pen(Color.Blue, 2 / 3);
            p2 = new Pen(Color.Red, 2 / 3);
            p3 = new Pen(Color.Green, 2 / 3);
            k = new KoordinatniSistem(250, 250, (double)numericUpDown5.Value);

            //Исцртавање 
            pictureBox1.BackColor = Color.White;

            //Исписивање у текстбокс
            numericUpDown6_ValueChanged(sender, e);
            numericUpDown1_ValueChanged(sender, e);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button3.Enabled = true;
            button4.Enabled = true;
            button5.Enabled = true;
            pictureBox1.Enabled = true;
            //Ресетовање лабела
            label9.Text = "Није квад. јед.";

            //Дефинисање
            k = new KoordinatniSistem(k.Xosa, k.Yosa, (double)numericUpDown5.Value);
            f = new Funkcija((int)numericUpDown6.Value, textBox1.Text);
            f2 = new Funkcija((int)numericUpDown1.Value, textBox2.Text);

            //Исцртавање координатног система и тачака функције
            k.Nacrtaj(g, p);
            if(checkBox2.Checked)
                f.Nacrtaj(g, k, p1);
            if(checkBox1.Checked)
                f2.Nacrtaj(g, k, p2);
            if (rez && checkBox3.Checked)
                f3.Nacrtaj(g, k, p3);

            //Налажење и исписивање корена
            UnesiUListBox(f, listBox1);
            UnesiUListBox(f2, listBox2);
            if (rez)
                UnesiUListBox(f3, listBox3);

            //Дискриминанта
            label9.Text = "D1 = " + Diskriminanta(f);
            label2.Text = "D2 = " + Diskriminanta(f2);
            if(rez)
                label5.Text = "D3 = " + Diskriminanta(f3);

            //Izvodi
            label13.Text = "P1'(x) = " + Funkcija.IspisFunkcije(~f);
            label14.Text = "P2'(x) = " + Funkcija.IspisFunkcije(~f2);
            if (rez)
                label15.Text = "P3'(x) = " + Funkcija.IspisFunkcije(~f3);
        }

        public string Diskriminanta(Funkcija f)
        {
            string d = "Nije kvad. jed.";
            if (f.Stepen == 2)
            {
                d = Convert.ToString(Math.Round(Math.Pow(f.Koef[1], 2) - (4 * f.Koef[2] * f.Koef[0]), 2));
            }
            return d;
        }

        //Координате курсора при клику и бул да ли је кликнуо или не
        double doleX = 0, doleY = 0;
        bool klik = false;
        //Пикчр бокс је кликнут и узимају се координате курсора
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            klik = true;
            doleX = e.X;
            doleY = e.Y;
            Cursor.Current = Cursors.SizeAll;
        }

        //Док је кликнут миш се помера и тако помера слику
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (klik)
            {
                k.Pomeraj(doleX, doleY, e.X, e.Y);
                k.Nacrtaj(g, p);

                button2.Enabled = true;
            }

            doleX = e.X;
            doleY = e.Y;
        }

        //Клик је пуштен и више не помера слику
        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if(checkBox2.Checked)
                f.Nacrtaj(g, k, p1);
            if(checkBox1.Checked)
                f2.Nacrtaj(g, k, p2);
            if (rez && checkBox3.Checked)
                f3.Nacrtaj(g, k, p3);
            klik = false;
            doleX = 0;
            doleY = 0;

            Cursor.Current = Cursors.Default;
        }

        //Oduzimanje polinoma
        private void button5_Click(object sender, EventArgs e)
        {
            f3 = f - f2;
            int stepen = Convert.ToInt32(f3.Stepen);
            textBox3.Text = Funkcija.IspisFunkcije(f3);
            rez = true;
        }

        //Mnozenje polinoma
        private void button4_Click(object sender, EventArgs e)
        {
            f3 = f * f2;
            int stepen = Convert.ToInt32(f3.Stepen);
            textBox3.Text = Funkcija.IspisFunkcije(f3);
            rez = true;
        }

        //Sabiranje polinoma
        private void button3_Click(object sender, EventArgs e)
        {
            f3 = f + f2;
            int stepen = Convert.ToInt32(f3.Stepen);
            textBox3.Text = Funkcija.IspisFunkcije(f3);
            rez = true;
        }

        //Увеличање
        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            //button1_Click(sender, e);
            button2.Enabled = true;
        }

        //Одабир степена полинома
        private void numericUpDown6_ValueChanged(object sender, EventArgs e)
        {
            int stepen = Convert.ToInt32(numericUpDown6.Value);
            textBox1.Text = "";
            for (int i = stepen; i >= 0; i--)
            {
                if(i == stepen)
                    textBox1.Text += "x^" + i;
                else
                    textBox1.Text += "+x^" + i;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            k.Jedinica = 30;
            numericUpDown5.Value = (decimal)k.Jedinica;
            k.Xosa = 250;
            k.Yosa = 250;
            button1_Click(sender, e);
            button2.Enabled = false;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            int stepen = Convert.ToInt32(numericUpDown1.Value);
            textBox2.Text = "";
            for (int i = stepen; i >= 0; i--)
            {
                if (i == stepen)
                    textBox2.Text += "x^" + i;
                else
                    textBox2.Text += "+x^" + i;
            }
        }

        private void UnesiUListBox(Funkcija f, ListBox listbox)
        {
            f.NadjiKorene();
            listbox.Items.Clear();
            for (int i = 0; i < f.Koreni.Count; i++)
            {
                if(f.Koreni[i] != 0)
                    listbox.Items.Add("x" + (i + 1) + " = " + f.Koreni[i]);
            }
        }
    }
}