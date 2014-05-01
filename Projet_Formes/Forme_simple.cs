using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Forme_simple : Forme
    {
        protected List<Point> _liste_points;
        protected string _couleur;

        public Forme_simple(int id, string nom, List<Point> liste_points, string couleur) : base (id, nom)
        {
            this._liste_points = liste_points;
            this._couleur = couleur;
        }

        public List<Point> Liste_points
        {
            get
            {
                return this._liste_points;
            }
            set
            {
                this._liste_points = value;
            }
        }

        public string Couleur
        {
            get
            {
                return this._couleur;
            }
            set
            {
                this._couleur = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.Out.WriteLine("Id : " + this._id);
            Console.Out.WriteLine("Couleur : " + this._couleur);
            foreach (Point liste_points in _liste_points)
            {
                Console.WriteLine("Point (" + liste_points.X + "," + liste_points.Y + ")");
            }
        }

        public override void translation(int x, int y)
        {
            foreach (Point liste_points in _liste_points)
            {
                liste_points.X += x;
                liste_points.Y += y;
            }
        }

        public override void homothetie(int coeff)
        {

        }
    }
}
