using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    class Forme_composee : Forme
    {
        protected Forme_simple []_tabforme;

        public Forme_composee(int id, string nom, Forme_simple []tabforme) : base(id, nom)
        {
            this._tabforme = tabforme;
        }

        public Forme_simple[] Tabforme
        {
            get
            {
                return this._tabforme;
            }
            set
            {
                this._tabforme = value;
            }
        }

        public override void Write()
        {
            
            base.Write();
            Console.Out.WriteLine("----------------------------");
            for (int x = 0; x < this._tabforme.Length; x++)
            {
                int numero = x+1;
                Console.Out.WriteLine("   Forme " + numero);
                this._tabforme[x].Write();
                Console.Out.WriteLine("----------------------------");
            }
        }

        public override void translation(int x, int y)
        {
            for (int z = 0; z < this._tabforme.Length; z++)
            {
                this._tabforme[z].translation(x, y);
            }
        }

        public override void homothetie(int coeff)
        {

        }
    }
}
