using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinEllipse : DessinFormeSimple
    {
        public override void dessiner(Forme_simple entry, Graphics g)
        {
            Type t = typeof(Ellipse);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                Ellipse e = (Ellipse)entry;
                SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
                g.FillEllipse(brush, e.Point1.X, e.Point1.Y, e.Largeur, e.Hauteur);
            }
            else if (successor != null)
            {
                successor.dessiner(entry, g);
            }
        }
    }
}
