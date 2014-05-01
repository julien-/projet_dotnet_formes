using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Rectangle : Forme_simple
    {
        public Rectangle(int id, string nom, string couleur, List<Point> listpoint) : base(id, nom, couleur, listpoint) { }
    }
}
