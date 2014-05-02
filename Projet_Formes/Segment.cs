using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Segment : Forme_simple
    {
        protected Point _point1;
        protected Point _point2;

        public Segment(int id, string nom, string couleur, Point point1, Point point2) : base(id, nom, couleur) 
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
    }
}
