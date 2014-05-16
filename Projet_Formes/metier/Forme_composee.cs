﻿using System;
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

        public override void translation(Point point1, Point point2)
        {
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
    }
}
