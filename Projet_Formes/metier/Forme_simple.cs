﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet_Formes
{
    public abstract class Forme_simple : Forme
    {
        protected int _couleur;

        public Forme_simple(int id, string nom, int couleur, int idgroupe) : base (id, nom, idgroupe)
        {
            this._couleur = couleur;
        }

        public int Couleur
        {
            get
            {
                return this._couleur;
            }
            set
            {
                this._couleur = value;
            }
        }

        public override void Write()
        {
            base.Write();
            Console.Out.WriteLine("Id : " + this._id);
            Console.Out.WriteLine("Couleur : " + this._couleur);    
        }

        public override void translation(Point vecteur) { }

        public override void homothetie(int zoom){}

        public abstract Boolean recuperer(int x, int y);

        public abstract void maj(Point point1, Point point2);

        public abstract void maj(Point[] tabpoints);
    }
}
