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

        public Forme_composee(int id, string nom) : base(id, nom)
        {
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

        public override void translation(Point point1, Point point2)
        {
            Point point3 = new Point { };
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                if (liste_formes.recuperer(point1.X, point1.Y))
                {
                    liste_formes.translation(point1, point2);
                }
                else
                {
                    liste_formes.translation_comp(point1, point2);
                }
            }
        }

        public override void homothetie(int zoom)
        {
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                liste_formes.homothetie(zoom);
            }
        }

        public override void maj(Point point1, Point point2) { }

        public override void maj(Point[] tabpoints) { }
    }
}
