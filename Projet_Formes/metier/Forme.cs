﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Projet_Formes
{
    public abstract class Forme
    {
        protected string _nom;
        protected int _groupe = -1;
        protected int _id = 0;

        public Forme(int id, string nom, int groupe)
        {
            this._id = id;
            this._nom = nom;
            this._groupe = groupe;
        }

        public string Nom
        {
            get
            {
                return this._nom;
            }
            set
            {
                this._nom = value;
            }
        }

        public int Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int IdGroupe
        {
            get
            {
                return this._groupe;
            }
            set
            {
                this._groupe = value;
            }
        }

        public virtual void Write()
        {
            Console.Out.WriteLine("Nom : " + this._nom);
        }

        public abstract void translation(Point vecteur);

        public abstract void homothetie(int zoom);
    }
}
