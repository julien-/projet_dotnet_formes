using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinRectangle : Dessiner<Rectangle>
    {
        public override void dessiner(Rectangle entry, Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillRectangle(brush, entry.Point1.X, entry.Point1.Y, entry.Largeur, entry.Hauteur);
        }
    }
}
