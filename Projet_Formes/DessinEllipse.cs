using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinEllipse : Dessiner<Ellipse>
    {
        public override void dessiner(Ellipse entry, Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
            g.FillEllipse(brush, entry.Point1.X, entry.Point1.Y, entry.Largeur, entry.Hauteur);
        }
    }
}
