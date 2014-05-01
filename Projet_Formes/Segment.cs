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
        public Segment(int id, string nom, List<Point> liste_points, string couleur) : base(id, nom, liste_points, couleur) { }
    }
}
