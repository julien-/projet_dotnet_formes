﻿using System;
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
            Type t = typeof(Triangle);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                Triangle tr = (Triangle)entry;
                SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
                g.FillPolygon(brush, tr.Tableau_points);
            }
            else if (successor != null)
            {
                successor.dessiner(entry, g);
            }
        }
    }
}
