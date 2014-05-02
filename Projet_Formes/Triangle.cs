using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Triangle : Polygone
    {
        public Triangle(int id, string nom, string couleur, Point[] tab_points) : base(id, nom, couleur, tab_points) 
        {
            this._nb_points = 3;

            if (!tab_points.Length.Equals(_nb_points))
                throw new System.ArgumentException("Nombre de points invalide");
        }
    }
}
