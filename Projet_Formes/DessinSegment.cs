using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinSegment : Dessiner<Segment>
    {
        public override void dessiner(Segment entry,  Graphics g)
        {
            Pen pen = new Pen(Color.Black, 10);
            g.DrawLine(pen, entry.Point1.X, entry.Point1.Y, entry.Point2.X, entry.Point2.Y);
        }
    }
}
