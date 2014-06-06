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

        public override void contourSelection(Forme_simple forme, Graphics g, Color couleur)
        {
            Type t = typeof(Ellipse);
            Type t2 = forme.GetType();
            if (t.Equals(t2))
            {
                Ellipse e = (Ellipse)forme;
                //Pen pen = new Pen(Color.FromArgb(255, 0, 255, 0), 10); //Couleur de selection
                Pen pen = new Pen(couleur, 10); //Couleur de selection
                g.DrawEllipse(pen, e.Point1.X, e.Point1.Y, e.Largeur, e.Hauteur);
            }
            else if (successor != null)
            {
                successor.contourSelection(forme, g, couleur);
            }
        }
    }
}
