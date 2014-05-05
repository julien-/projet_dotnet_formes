﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinPolygone : DessinFormeSimple
    {
        public override void dessiner(Forme_simple entry, Graphics g)
        {
            Polygone p = (Polygone)entry;
            SolidBrush brush = new SolidBrush(Color.FromArgb(entry.Couleur));
            g.FillPolygon(brush, p.Tableau_points);
        }
    }
}