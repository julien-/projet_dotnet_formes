using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinTriangle : Dessiner<Triangle>
    {
        public override void dessiner(Triangle entry, Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
            g.FillPolygon(brush, entry.Tableau_points);
        }
    }
}
