using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Triangle : Polygone
    {
        public Triangle(int id, string nom, Point point1, Point point2, string couleur, Point []tabcoord) : base (id, nom, point1, point2, couleur, tabcoord){}
    }
}
