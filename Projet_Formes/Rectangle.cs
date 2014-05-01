using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Rectangle : Forme_simple
    {
        public Rectangle(int id, string nom, List<Point> liste_points, string couleur) : base(id, nom, liste_points, couleur) 
        {
            this._nombre_points = 2;

            if(!liste_points.Count.Equals(_nombre_points))
                throw new System.ArgumentException("Nombre de points invalide");
        }
    }
}
