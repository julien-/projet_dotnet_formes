using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public class Segment : Forme_simple
    {
        protected Point _point1;
        protected Point _point2;

        public Segment(int id, string nom, int couleur, Point point1, Point point2) : base(id, nom, couleur) 
        {
            this._point1 = point1;
            this._point2 = point2;
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

        public override void Write()
        {
            base.Write();
            Console.WriteLine("Point 1 : (" + this._point1.X + "," + this._point1.Y + ")");
            Console.WriteLine("Point 2 : (" + this._point2.X + "," + this._point2.Y + ")");
        }

        public override void translation(int x, int y)
        {
            _point1.X += x;
            _point1.Y += y;
            _point2.X += x;
            _point2 .Y += y;
        }

        public override void maj(Point point1, Point point2)
        {
            this._point1 = point1;
            this._point2 = point2;
        }

        public override void homothetie(int zoom)
        {
            //recherche du centre du segment
            Point centre = new Point(0, 0);
            centre.X += this._point1.X;
            centre.Y += this._point1.Y;
            centre.X += this._point2.X;
            centre.Y += this._point2.Y;
            centre.X /= 2;
            centre.Y /= 2;

            //HOMETHETIE
            //Gestion des X
            if (this._point1.X < centre.X)
            {//partie gauche
                this._point1.X -= zoom;
                this._point2.X += zoom;
            }
            else
            {//partie droite
                this._point1.X += zoom;
                this._point2.X -= zoom;
            }

            //Gestion des Y
            if (this._point1.Y < centre.Y)
            {//partie haute
                this._point1.Y -= zoom;
                this._point2.Y += zoom;
            }
            else
            {//partie basse
                this._point1.Y += zoom;
                this._point2.Y -= zoom;
            }
        }

    }
}
