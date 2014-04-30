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
            public Segment(int id, string nom, Point point1, Point point2, string couleur) : base (id, nom, point1, point2, couleur){}
    }
}
