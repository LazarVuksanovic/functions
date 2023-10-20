using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funkcije
{
    public class Tacka
    {
        private double x;
        private double y;
        private double r = 2.3;

        public Tacka()
        {
            x = 0;
            y = 0;
        }

        public Tacka(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Tacka(double x, double y, int r)
        {
            this.x = x;
            this.y = y;
            this.r = r;
        }

        public Tacka(Tacka A)
        {
            this.x = A.x;
            this.y = A.y;
            this.r = A.r;
        }

        public double X
        {
            get { return x; }
            set { this.x = value; }
        }

        public double Y
        {
            get { return y; }
            set { this.y = value; }
        }

        public double R
        {
            get { return r; }
            set { this.r = value; }
        }

        public static Tacka PretvoriUTacku(Tacka A, KoordinatniSistem k)
        {
            A.Y = k.Yosa - A.Y * k.Jedinica;
            A.X = k.Xosa + A.X * k.Jedinica;
            return A;
        }
    }
}
