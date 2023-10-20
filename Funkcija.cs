using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Funkcije
{
    public class Funkcija
    {
        private double[] koef;
        private List<double> koreni;
        private int stepen;

        public Funkcija(int n)
        {
            this.stepen = n;
            this.koef = new double[stepen + 1];
            this.koreni = new List<double>(this.stepen);
        }

        public Funkcija(int stepen, string funkcija)
        {
            this.stepen = stepen;
            this.koef = new double[stepen + 1];
            this.koreni = new List<double>(this.stepen);
            for (int i = stepen; i >= 0; i--)
            {
                if (funkcija.Substring(0, funkcija.IndexOf('x')) == "")
                    koef[i] = 1;
                else
                {
                    try
                    {
                        koef[i] = float.Parse(funkcija.Substring(0, funkcija.IndexOf('x')));
                    }
                    catch
                    {
                        if (funkcija.Substring(0, funkcija.IndexOf('x')).Contains('/'))
                        {
                            string broj = funkcija.Substring(0, funkcija.IndexOf('x'));
                            int a = Convert.ToInt32(broj.Substring(0, funkcija.IndexOf('/')));
                            int b = Convert.ToInt32(broj.Substring(funkcija.IndexOf('/') + 1));
                            koef[i] = (double)a / (double)b;
                        }
                    }
                }
                funkcija = funkcija.Substring(funkcija.IndexOf('+') + 1);
            }
        }

        public double[] Koef
        {
            get { return this.koef; }
            set { this.koef = value; }
        }

        public int Stepen
        {
            get { return this.stepen; }
            set { this.stepen = value; }
        }

        public List<double> Koreni
        {
            get { return this.koreni; }
            set { this.koreni = value; }
        }

        public void Nacrtaj(Graphics g, KoordinatniSistem k, Pen p)
        {
            Tacka trenutna;
            Tacka prethodna = new Tacka();
            double y = 0;

            for (double i = -20; i < 20; i += 0.1)
            {
                for (int j = 0; j < stepen + 1; j++)
                {
                    y += this.koef[j] * Math.Pow(i, j);
                }
                trenutna = Tacka.PretvoriUTacku(new Tacka(i, y), k);

                if (i == -20)
                    prethodna = new Tacka(trenutna);

                try
                {
                    g.DrawLine(p, (float)trenutna.X, (float)trenutna.Y, (float)prethodna.X, (float)prethodna.Y);
                }
                catch { }

                prethodna = new Tacka(trenutna);
                y = 0;
            }
        }

        private double F(double x)
        {
            double y = 0;
            for (int i = 0; i < stepen + 1; i++)
            {
                y += this.koef[i] * Math.Pow(x, i);
            }
            return y;
        }

        private void MetodaPolovljenja(double levo, double desno)
        {
            if (F(levo) * F(desno) > 0)
                return;

            double sredina = levo;
            while(desno - levo > 0.01)
            {
                sredina = (levo + desno) / 2;
                if (F(sredina) == 0.0)
                    break;

                else if (F(sredina) * F(levo) < 0)
                    desno = sredina;

                else
                    levo = sredina;
            }

            this.koreni.Add(Math.Round(sredina, 1));
        }
        public void NadjiKorene()
        {
            double levo, desno;
            koreni.Clear();
            for (double i = -100; i < 100; i += 0.01)
            {
                levo = i;
                desno = levo + 0.01;

                MetodaPolovljenja(levo, desno);
                if (stepen == koreni.Count)
                    break;
            }
        }

        public double this[int i]
        {
            get { if (i < 0 || i > Stepen) return 0; return koef[i]; }
        }

        public static Funkcija operator +(Funkcija P, Funkcija Q)
        {
            Funkcija C = new Funkcija(Math.Max(P.Stepen, Q.Stepen));
            for (int i = 0; i <= C.Stepen; i++)
            {
                C.koef[i] = P[i] + Q[i];
            }
            return C;
        }

        public static Funkcija operator -(Funkcija P, Funkcija Q)
        {
            Funkcija C = new Funkcija(Math.Max(P.Stepen, Q.Stepen));
            for (int i = 0; i <= C.Stepen; i++)
            {
                C.koef[i] = P[i] - Q[i];
            }
            return C;
        }

        public static Funkcija operator *(Funkcija P, Funkcija Q)
        {
            Funkcija C = new Funkcija(P.Stepen + Q.Stepen);
            for (int i = P.stepen; i >=0; i--)
            {
                for (int j = Q.Stepen; j >= 0; j--)
                {
                    C.koef[i + j] += P[i] * Q[j];
                }
            }
            return C;
        }

        //Izvod funkcije
        public static Funkcija operator ~(Funkcija P)
        {
            Funkcija C = new Funkcija(P.Stepen);
            for (int i = 1; i <= P.Stepen; i++)
            {
                C.koef[i - 1] = i * P[i];
            }
            return C;
        }

        public static string IspisFunkcije(Funkcija f)
        {
            string s = "";
            for (int i = f.stepen; i >= 0; i--)
            {
                if (f.koef[i] == 0)
                    continue;

                if (i == f.stepen)
                {
                    if(f.koef[i] == 1)
                        s += "x^" + i;
                    else s += f.koef[i] + "x^" + i;
                }

                else
                {
                    if(f.koef[i] == 1)
                        s +="+" + "x^" + i;
                    else
                        s += "+" + f.koef[i] + "x^" + i;
                }
            }
            return s;
        }
    }
}
