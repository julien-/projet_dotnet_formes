using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Rectangle : Forme_simple
    {
           public Rectangle(string nom, Point point1, Point point2, int couleur) : base (nom, point1, point2, couleur){}
    }
}
