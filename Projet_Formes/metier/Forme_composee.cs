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
        protected List<Forme> _liste_formes;

        public Forme_composee(int id, string nom, int idgroupe, List<Forme> listformes) : base(id, nom, idgroupe)
        {
            this._liste_formes = listformes;
        }

        public List<Forme> Liste_formes
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

        public override void translation(Point vecteur)
        {
            foreach (Forme_simple formes in _liste_formes)
            {
                formes.translation(vecteur);
            }
        }

        public override void homothetie(int zoom)
        {
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                liste_formes.homothetie(zoom);
            }
        }
    }
}
