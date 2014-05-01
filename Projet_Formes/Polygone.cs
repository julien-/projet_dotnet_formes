using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Polygone : Forme_simple
    {

        public Polygone(int id, string nom, string couleur, List<Point> tabcoord) : base (id, nom , couleur, tabcoord)
        {
        }

        public override void Write()
        {
            base.Write();
            for (int x = 0; x < this._tabcoord.Count; x++)
            {
                int Number = x + 3;
                Console.Out.WriteLine("Point" + Number + " (" + this._tabcoord[x].X + "," + this._tabcoord[x].Y + ")");
            }
            
        }

        public override void translation(float x, float y)
        {
            base.translation(x, y);

            for (int z = 0; z < this._tabcoord.Count; z++)
            {
                this._tabcoord[z].X = this._tabcoord[z].X + x;
                this._tabcoord[z].Y = this._tabcoord[z].Y + y;
            }
        }
    }
}
