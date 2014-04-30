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
        protected Point _point1;
        protected Point _point2;
        protected string _couleur;

        public Forme_simple(int id, string nom, Point point1, Point point2, string couleur) : base (id, nom)
        {
            this._point1 = point1;
            this._point2 = point2;
            this._couleur = couleur;
        }

        public Point Point1
        {
            get
            {
                return this._point1;
            }
            set
            {
                this._point1 = value;
            }
        }

        public Point Point2
        {
            get
            {
                return this._point2;
            }
            set
            {
                this._point2 = value;
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
            Console.Out.WriteLine("Point1 (" + this._point1.X + "," + this._point1.Y + ")");
            Console.Out.WriteLine("Point2 (" + this._point2.X + "," + this._point2.Y + ")");
        }

        public override void translation(int x, int y)
        {
            this._point1.X = this._point1.X + x;
            this._point1.Y = this._point1.Y + y;

            this._point2.X = this._point2.X + x;
            this._point2.Y = this._point2.Y + y;
        }

        public override void homothetie(int coeff)
        {

        }
    }
}
