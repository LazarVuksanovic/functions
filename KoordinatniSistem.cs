using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Funkcije
{
    public class KoordinatniSistem
    {
        double xosa, yosa;
        double jedinica;
        public KoordinatniSistem(double x, double y, double jedinica)
        {
            xosa = x;
            yosa = y;
            this.jedinica = jedinica;
        }

        public double Xosa
        {
            get { return xosa; }
            set { this.xosa = value; }
        }

        public double Yosa
        {
            get { return yosa; }
            set { this.yosa = value; }
        }

        public double Jedinica
        {
            get { return jedinica; }
            set { this.jedinica = value; }
        }

        public void Nacrtaj(Graphics g, Pen p)
        {
            g.Clear(Color.White);
            //y
            g.DrawLine(p, 0, (float)this.yosa, 500, (float)this.yosa);
            //x
            g.DrawLine(p, (float)this.xosa, 0, (float)this.xosa, 500);
        }

        public void Pomeraj(double dolex, double doley, double gorex, double gorey)
        {
            this.xosa += (gorex - dolex);
            this.yosa += (gorey - doley);
        }
    }
}
