using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Triangle : Polygone
    {
        public Triangle(int id, string nom, string couleur, List<Point> tabcoord) : base (id, nom, couleur, tabcoord){}
    }
}
