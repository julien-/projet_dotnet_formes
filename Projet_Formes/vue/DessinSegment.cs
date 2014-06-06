using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class DessinSegment : DessinFormeSimple
    {
        public override void dessiner(Forme_simple entry, Graphics g)
        {
            Type t = typeof(Segment);
            Type t2 = entry.GetType();
            if (t.Equals(t2))
            {
                Segment s = (Segment)entry;
                Pen pen = new Pen(Color.FromArgb(entry.Couleur), 4);
                g.DrawLine(pen, s.Point1.X, s.Point1.Y, s.Point2.X, s.Point2.Y);
            }
            else if (successor != null)
            {
                successor.dessiner(entry, g);
            }
        }

        public override void contourSelection(Forme_simple forme, Graphics g, Color couleur)
        {
            Type t = typeof(Segment);
            Type t2 = forme.GetType();
            if (t.Equals(t2))
            {
                Segment s = (Segment)forme;
                Pen pen = new Pen(couleur, 10); //Couleur de selection
                g.DrawLine(pen, s.Point1.X, s.Point1.Y, s.Point2.X, s.Point2.Y);
            }
            else if (successor != null)
            {
                successor.contourSelection(forme, g, couleur);
            }
        }
    }
}
