using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinTriangle : DessinFormeSimple
    {
        public override void dessiner(Forme_simple entry, Graphics g)
        {
            Triangle t = (Triangle)entry;
            SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
            g.FillPolygon(brush, t.Tableau_points);
        }
    }
}
