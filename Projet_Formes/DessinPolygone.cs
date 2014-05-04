using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinPolygone : Dessiner<Polygone>
    {
        public override void dessiner(Polygone entry, Graphics g)
        {
            SolidBrush brush = new SolidBrush(Color.Black);
            g.FillPolygon(brush, entry.Tableau_points);
        }
    }
}
