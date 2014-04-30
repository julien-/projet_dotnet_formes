using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Polygone : Forme_simple
    {
        protected Point []_tabcoord;

        public Polygone(int id, string nom, Point point1, Point point2, string couleur, Point []tabcoord) : base (id, nom, point1, point2, couleur)
        {
            this._tabcoord = tabcoord;
        }

        public Point []Tabcoord
        {
            get
            {
                return this._tabcoord;
            }
            set
            {
                this._tabcoord = value;
            }
        }

        public override void Write()
        {
            base.Write();
            for (int x = 0; x < this._tabcoord.Length; x++)
            {
                int Number = x + 3;
                Console.Out.WriteLine("Point" + Number + " (" + this._tabcoord[x].X + "," + this._tabcoord[x].Y + ")");
            }
            
        }

        public override void translation(float x, float y)
        {
            base.translation(x, y);

            for (int z = 0; z < this._tabcoord.Length; z++)
            {
                this._tabcoord[z].X = this._tabcoord[z].X + x;
                this._tabcoord[z].Y = this._tabcoord[z].Y + y;
            }
        }
    }
}
