using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public class Polygone : Forme_simple
    {
        protected Point []_tab_points;

        public Polygone(int id, string nom, int couleur, Point []tab_points) : base(id, nom, couleur)
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

        public override void Write()
        {
            base.Write();
            for(int i = 0; i < _tab_points.Length ; i++)
            {
                int Nombre = i + 1;
                Console.WriteLine("Point " + Nombre + " : (" + this._tab_points[i].X + "," + this._tab_points[i].Y + ")");
            }
        }

        public override void translation(Point point1, Point point2)
        {
            for (int i = 0; i < this._tab_points.Length; i++)
            {
                if ((point1.X >= this._tab_points[i].X - 4 && point1.X <= this._tab_points[i].X + 4) && (point1.Y >= this._tab_points[i].Y - 4 && point1.Y <= this._tab_points[i].Y + 4))
                {
                    this._tab_points[i] = point2;
                }
                else
                {
                    //this._tab_points[i].X = point2.X + ;
                    //this._tab_points[i].Y = point2.Y + 10;
                }
            }
        }

        public override void maj(Point[] tabpoints) 
        {
            this._tab_points = tabpoints;
        }

        public override void homothetie(int zoom)
        {
            //recherche du centre du triangle
            Point centre = new Point();
            centre = this.centre();

            for (int i = 0; i < this._tab_points.Length; i++)
            {
                //Gestion des X
                if (this._tab_points[i].X < centre.X)
                {//partie gauche
                    this._tab_points[i].X -= zoom;
                }
                else
                {//partie droite
                    this._tab_points[i].X += zoom;
                }

                //Gestion des Y
                if (this._tab_points[i].Y < centre.Y)
                {//partie haute
                    this._tab_points[i].Y -= zoom;
                }
                else
                {//partie basse
                    this._tab_points[i].Y += zoom;
                }
            }
        }

        private Point centre()
        {//recherche du centre du triangle
            Point centre = new Point(0, 0);

            for (int i = 0; i < this._tab_points.Length; i++)
            {
                centre.X += this._tab_points[i].X;
                centre.Y += this._tab_points[i].Y;
            }
            centre.X /= this._tab_points.Length;
            centre.Y /= this._tab_points.Length;

            return centre;
        }

        public override Boolean recuperer(int x, int y)
        {
            Boolean trouve = false;
            for (int i = 0; i < this._tab_points.Length; i++)
            {
                if ((this._tab_points[i].X <= x + 2 || this._tab_points[i].X >= x + 2) && (this._tab_points[i].Y <= y + 2 || this._tab_points[i].Y >= y + 2))
                {
                    trouve = true;
                    break;
                }
            }
            return trouve;
        }

    }
}
