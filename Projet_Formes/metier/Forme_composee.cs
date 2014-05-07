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
            foreach (Forme_simple liste_formes in _liste_formes)
            {
                liste_formes.translation(point1, point2);
            }
        }

        public override void homothetie(int zoom)
        {

        }

        public override void maj(Point point1, Point point2) { }

        public override void maj(Point[] tabpoints) { }
    }
}
