using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Polygone : Forme_simple
    {
        protected Point []_tab_points;
        protected int _nb_points;

        public Polygone(int id, string nom, string couleur, Point []tab_points) : base(id, nom, couleur)
        {
            this._tab_points = tab_points;
        }

        public Point []Tableau_points
        {
            get
            {
                return this._tab_points;
            }
            set
            {
                this._tab_points = value;
            }
        }

        public int Nombre_points
        {
            get
            {
                return this._nb_points;
            }
            set
            {
                this._nb_points = value;
            }
        }

        public override void Write()
        {
            base.Write();
            for(int i = 0; i < _tab_points.Length ; i++)
            {
                int Nombre = i + 1;
                Console.WriteLine("Point " + Nombre + " : (" + this._tab_points[i].X + "," + this._tab_points[i].Y + ")");
            }
        }

        public override void translation(int x, int y)
        {
            for (int i = 0; i < _tab_points.Length; i++)
            {
                this._tab_points[i].X += x;
                this._tab_points[i].Y += y;
            }
        }
    }
}
