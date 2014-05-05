using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    class Forme_composee : Forme
    {
        protected List<Forme_simple> _liste_formes;

        public Forme_composee(int id, string nom, List<Forme_simple> liste_formes)
            : base(id, nom)
        {
            this._liste_formes = liste_formes;
        }

        public List<Forme_simple> Liste_formes
        {
            get
            {
                return this._liste_formes;
            }
            set
            {
                this._liste_formes = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.Out.WriteLine("----------------------------");
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                liste_formes.Write();
                Console.Out.WriteLine("----------------------------");
            }
        }
        public override void maj(int x, int y) { }
        public override void translation(int x, int y)
        {
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                liste_formes.translation(x, y);
            }
        }

        public override void homothetie(int coeff)
        {

        }
    }
}
