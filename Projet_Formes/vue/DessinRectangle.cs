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

        public override void contourSelection(Forme_simple forme, Graphics g, Color couleur)
        {
            Type t = typeof(Rectangle);
            Type t2 = forme.GetType();
            if (t.Equals(t2))
            {
                Rectangle e = (Rectangle)forme;
                Pen pen = new Pen(couleur, 10); //Couleur de selection
                g.DrawRectangle(pen, e.Point1.X, e.Point1.Y, e.Largeur, e.Hauteur);
            }
            else if (successor != null)
            {
                successor.contourSelection(forme, g, couleur);
            }
        }
    }
}
