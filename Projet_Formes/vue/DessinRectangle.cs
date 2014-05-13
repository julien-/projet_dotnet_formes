using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinRectangle : DessinFormeSimple
    {
        public override void dessiner(Forme_simple entry, Graphics g)
        {
            Type t = typeof(Rectangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                Rectangle r = (Rectangle)entry;
                SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
                g.FillRectangle(brush, r.Point1.X, r.Point1.Y, r.Largeur, r.Hauteur);
            }
            else if (successor != null)
            {
                successor.dessiner(entry, g);
            }
        }
    }
}
