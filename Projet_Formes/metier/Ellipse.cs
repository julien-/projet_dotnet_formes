using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public class Ellipse : Forme_simple
    {
        protected Point _point1;
        protected int _hauteur;
        protected int _largeur;

        
        public Ellipse(int id, string nom, int couleur, Point point1, int hauteur, int largeur) : base(id, nom, couleur)
        {
            this._point1 = point1;
            this._hauteur = hauteur;
            this._largeur = largeur;
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

        public int Hauteur
        {
            get
            {
                return this._hauteur;
            }
            set
            {
                this._hauteur = value;
            }
        }

        public int Largeur
        {
            get
            {
                return this._largeur;
            }
            set
            {
                this._largeur = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.WriteLine("Point 1 : (" + this._point1.X + "," + this._point1.Y + ")");
            Console.WriteLine("Hauteur : " + this._hauteur);
            Console.WriteLine("Largeur : " + this._largeur);
        }

        public override void translation(int x, int y)
        {
            _point1.X += x;
            _point1.Y += y;
        }

        public override void maj(Point point1, Point point2)
        {
            this._point1 = point1;
            this._largeur = point2.X - point1.X;
            this._hauteur = point2.Y - point1.Y;
        }        
    }


}
