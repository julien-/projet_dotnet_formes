using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public class Triangle : Polygone
    {
        public Triangle(int id, string nom, int couleur, Point[] tab_points) : base(id, nom, couleur, tab_points) 
        {
            if (!tab_points.Length.Equals(3))
                throw new System.ArgumentException("Nombre de points invalide");
        }

        /*public override void translation(Point point1, Point point2)
        {
            base.translation(point1, point2);
        }*/

        /*public override void maj(Point[] tabpoints)
        {
            base.maj(tabpoints);
        }*/

        public override void homothetie(int zoom)
        {
            //recherche du centre du triangle
            Point centre = new Point(0, 0);

            for (int i = 0; i < 3; i++)
            {
                centre.X += this._tab_points[i].X;
                centre.Y += this._tab_points[i].Y;
            }
            centre.X /= 3;
            centre.Y /= 3;

            //HOMOTHETIE
            for (int i = 0; i < 3; i++)
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
        /*public override Boolean recuperer(int x, int y)
        {
            return base.recuperer(x, y);
        }*/
    }
}
